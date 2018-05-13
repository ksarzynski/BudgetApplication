using BudgetApplication.Data;
using BudgetApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetApplication.Repository
{
    public interface ISubcategoriesRepository
    {
        Task<IList<Subcategory>> GetAllAsync();
        Task<IList<Subcategory>> GetAllForUserID(string userID);
        Task<Subcategory> Get(int? id);
        void Insert(Subcategory model);
        void Update(Subcategory model);
        void Delete(Subcategory model);
        bool SubcategoryExists(int id);
    }
    public class SubcategoriesRepository : ISubcategoriesRepository 
    {
        private readonly ApplicationDbContext _context;
        private DbSet<Subcategory> _entity;

        public SubcategoriesRepository(ApplicationDbContext context)
        {
            _context = context;
            _entity = _context.Set<Subcategory>();
        }

        public async Task<IList<Subcategory>> GetAllAsync()
        {
            var listOfSubcategories = await _context.Subcategories.Include(x => x.Category).ToListAsync();
            return listOfSubcategories;
        }

        public async Task<IList<Subcategory>> GetAllForUserID(string userID)
        {
            if (userID.Trim().Length == 0 || String.IsNullOrEmpty(userID)) throw new ArgumentException("User Id is null or empty");
            var result = await _entity.Where(x => x.UserID == userID).ToListAsync();

            return result;
        }

        public async Task<Subcategory> Get(int? id)
        {
            var subcategory = await _entity.SingleOrDefaultAsync(s => s.SubcategoryID == id);
            return subcategory;
        }

        public void Insert(Subcategory entity)
        {
            if (entity != null)
            {
                _entity.Add(entity);
                _context.SaveChanges();
                
            }
            else throw new ArgumentNullException("There was a problem with subcategories entity.");
        }

        public void Update(Subcategory entity)
        {
            if (entity != null && SubcategoryExists(entity.SubcategoryID))
            {
                _entity.Update(entity);
                _context.SaveChanges();
            }
            else throw new ArgumentNullException("There was a problem with subcategories entity.");
        }

        public void Delete(Subcategory entity)
        {
            if (entity != null && SubcategoryExists(entity.SubcategoryID))
            {
                _entity.Remove(entity);
                _context.SaveChanges();
            }
            else throw new ArgumentNullException("There was a problem with Entity.");
        }

        public bool SubcategoryExists(int id)
        {
            return _entity.Any(e => e.SubcategoryID == id);
        }
    }
}
