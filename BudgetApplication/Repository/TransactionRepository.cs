using BudgetApplication.Data;
using BudgetApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetApplication.Repository
{
    public interface ITransactionsRepository
    {
        Task<IList<Transaction>> GetAllAsync();
        Task<IList<Transaction>> GetAllForUserID(string userID);
        Task<Transaction> Get(int? id);
        void Insert(Transaction model);
        void Update(Transaction model);
        void Delete(Transaction model);
        bool TransactionExists(int id);
    }
    public class TransactionRepository : ITransactionsRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<Transaction> _entity;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
            _entity = _context.Set<Transaction>();
        }

        public void Delete(Transaction model)
        {
            if (model != null)
            {
                _entity.Remove(model);
                _context.SaveChanges();
            }
            else throw new ArgumentNullException("There was a problem with Transaction entity.");
        }

        public async Task<Transaction> Get(int? id)
        {
            var transaction = await _entity.SingleOrDefaultAsync(s => s.TransactionID == id);
            return transaction;
        }

        public async Task<IList<Transaction>> GetAllAsync()
        {
            var listOfTransactions = await _context.Transactions.Include(x => x.Item).ToListAsync();
            return listOfTransactions;
        }

        public async Task<IList<Transaction>> GetAllForUserID(string userID)
        {
            if (userID.Trim().Length == 0 || String.IsNullOrEmpty(userID)) throw new ArgumentException("User Id is null or empty");
            var result = await _entity.Where(x => x.UserID == userID).ToListAsync();

            return result;
        }

        public void Insert(Transaction model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("There was a problem with Transaction entity.");
            }
            _entity.Add(model);
            _context.SaveChanges();
        }

        public bool TransactionExists(int id)
        {
            return _entity.Any(e => e.TransactionID == id);
        }

        public void Update(Transaction model)
        {
            if (model != null)
            {
                _entity.Update(model);
                _context.SaveChanges();
            }
            else throw new ArgumentNullException("There was a problem with Transaction entity.");
        }
    }
}
