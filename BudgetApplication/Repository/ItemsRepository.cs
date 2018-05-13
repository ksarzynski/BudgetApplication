using BudgetApplication.Data;
using BudgetApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetApplication.Repository
{
    public interface IItemsRepository
    {
        Task<IList<Item>> GetAllAsync();
        Task<Item> Get(int? id);
        void Insert(Item model);
        void Update(Item model);
        void Delete(Item model);
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

        public async Task<Item> Get(int? id)
        {
            var item = await _entity.SingleOrDefaultAsync(s => s.ItemID == id);
            return item;
        }

        public void Insert(Item entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException("There was a problem with subcategories entity.");
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
                throw new System.ArgumentNullException("There was a problem with subcategories entity.");
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
                throw new System.ArgumentNullException("There was a problem with Entity.");
            }
        }
    }
}
