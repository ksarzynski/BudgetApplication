using BudgetApplication.Data;
using BudgetApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetApplication.Repository
{
    public interface IItemsRepository
    {
        Task<IList<Item>> GetAllAsync();
        Task<IList<Item>> GetAllForUserID(string userID);
        Task<Item> Get(int? id);
        void Insert(Item model);
        void Update(Item model);
        void Delete(Item model);
        bool ItemExists(int id);
    }
    public class ItemsRepository : IItemsRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<Item> _entity;

        public ItemsRepository(ApplicationDbContext context)
        {
            _context = context;
            _entity = _context.Set<Item>();
        }
        public async Task<IList<Item>> GetAllAsync()
        {
            var listOfItems = await _context.Items.Include(x => x.Subcategory).ToListAsync();
            return listOfItems;
        }
        public async Task<IList<Item>> GetAllForUserID(string userID)
        {
            if (userID.Trim().Length == 0 || String.IsNullOrEmpty(userID)) throw new ArgumentException("User Id is null or empty");
            var result = await _entity.Where(x => x.UserID == userID).ToListAsync();

            return result;
        }

        public async Task<Item> Get(int? id)
        {
            var item = await _entity.SingleOrDefaultAsync(s => s.ItemID == id);
            return item;
        }

        public void Insert(Item entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("There was a problem with items entity.");
            }
            _entity.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Item entity)
        {
            if (entity != null)
            {
                _entity.Update(entity);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException("There was a problem with items entity.");
            }

        }

        public void Delete(Item entity)
        {
            if (entity != null)
            {
                _entity.Remove(entity);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException("There was a problem with items Entity.");
            }
        }
        public bool ItemExists(int id)
        {
            return _entity.Any(e => e.ItemID == id);
        }
    }
}
