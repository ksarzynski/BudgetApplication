using BudgetApplication.Data;
using BudgetApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BudgetApplication.Repository
{
    public interface ICategoriesRepository
    {
        Task<IList<Category>> GetAll();
        Task<IList<Category>> GetAllForUserID(string userID);
        Task<Category> Get(int? id);
        void Insert(Category model);
        void Update(Category model);
        void Delete(Category model);
        bool CategoryExists(int id);
    }

    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<Category> _entity;

        public CategoriesRepository(ApplicationDbContext context)
        {
            _context = context;
            _entity = _context.Set<Category>();
        }

        public async Task<IList<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync(); 
        }

        public async Task<Category> Get(int? id)
        {
            return await _entity.SingleOrDefaultAsync(s => s.CategoryID == id);
        }

        public async Task<IList<Category>> GetAllForUserID(string userID)
        {
            if (userID.Trim().Length == 0 || String.IsNullOrEmpty(userID)) throw new ArgumentException("User Id is null or empty");
            var result = await _entity.Where(x => x.UserID == userID).ToListAsync();

            return result;
        }

        public void Insert(Category entity)
        {
            if (entity != null)
            {
                _entity.Add(entity);
                _context.SaveChanges();
            }
            else throw new ArgumentNullException("There was a problem with Entity.");
        }

        public void Update(Category entity)
        {
            if (entity != null)
            {
                _entity.Update(entity);
                _context.SaveChanges();
            }
            else throw new ArgumentNullException("There was a problem with Entity.");            
        }

        public void Delete(Category entity)
        {
            if (entity != null && CategoryExists(entity.CategoryID))
            {
                _entity.Remove(entity);
                _context.SaveChanges();   
            }
            else
            {
                throw new ArgumentNullException("There was a problem with Entity.");
            }
        }
        public bool CategoryExists(int id)
        {
            return _entity.Any(e => e.CategoryID == id);
        }

    }
}
