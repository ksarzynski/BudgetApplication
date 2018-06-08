using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetApplication.Models;

namespace BudgetApplication.Repository
{
    public class FakeCategoriesRepository : ICategoriesRepository
    {
        private List<Category> categories = new List<Category>();

        public async Task<IList<Category>> GetAll()
        {
            return await Task.Run(() => categories);
        }
        public async Task<IList<Category>> GetAllForUserID(string userID)
        {
            return await Task.Run(() => categories.Where(c => c.UserID.Contains(userID)).ToList());
        }

        public async Task<Category> Get(int? id)
        {
            return await Task.Run(() => categories.FirstOrDefault(c => c.CategoryID == id));
        }

        public void Insert(Category model)
        {
            categories.Add(model);
        }

        public void Update(Category model)
        {
            var c = categories.FirstOrDefault(x => x.CategoryID == model.CategoryID);
            c = model;
        }

        public void Delete(Category model)
        {
            var c = categories.FirstOrDefault(x => x.CategoryID == model.CategoryID);
            categories.Remove(c);
        }

        public bool CategoryExists(int id)
        {
            return categories.Any(c => c.CategoryID == id);
        }
    }
}
