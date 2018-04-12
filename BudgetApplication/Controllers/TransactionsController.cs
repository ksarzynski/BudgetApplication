using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BudgetApplication.Data;
using BudgetApplication.Models;
using System.Security.Claims;
using BudgetApplication.Roles;
using Microsoft.AspNetCore.Authorization;

namespace BudgetApplication.Controllers
{
    [Authorize(Roles = "Administrator, User")]
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;


        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var applicationDbContext = _context.Transactions.Include(t => t.Item).Where(x => x.UserID == userId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Item)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["ItemID"] = new SelectList(_context.Items.Where(x => x.UserID == userId), "ItemID", "ItemName");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Transaction transaction)
      
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var values = new Transaction
                {
                    Id = transaction.Id,
                    ItemID = transaction.ItemID,
                    Price = transaction.Price,
                    TransactionDate = transaction.TransactionDate,
                    TransactionPlace = transaction.TransactionPlace,
                    ExchangeRate = transaction.ExchangeRate,
                    UserID = userId

                };
                _context.Add(values);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var userId2 = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["ItemID"] = new SelectList(_context.Items.Where(x => x.UserID == userId2), "ItemID", "ItemID", transaction.ItemID);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.SingleOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["ItemID"] = new SelectList(_context.Items.Where(x => x.UserID == userId), "ItemID", "ItemName", transaction.ItemID);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var values = new Transaction
                    {
                        Id = transaction.Id,
                        ItemID = transaction.ItemID,
                        Price = transaction.Price,
                        TransactionDate = transaction.TransactionDate,
                        TransactionPlace = transaction.TransactionPlace,
                        ExchangeRate = transaction.ExchangeRate,
                        UserID = userId
                    };
                    _context.Update(values);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var userId2 = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["ItemID"] = new SelectList(_context.Items.Where(x => x.UserID == userId2), "ItemID", "ItemName", transaction.ItemID);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Item)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.SingleOrDefaultAsync(m => m.Id == id);
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }
}
