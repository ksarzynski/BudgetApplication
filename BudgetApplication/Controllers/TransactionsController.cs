using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BudgetApplication.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using BudgetApplication.Repository;

namespace BudgetApplication.Controllers
{
    [Authorize(Roles = "User")]
    public class TransactionsController : Controller
    {
        private readonly ITransactionsRepository _transactionsRepository;
        private readonly IItemsRepository _itemsRepository;

        public TransactionsController(ITransactionsRepository transactionsRepository, IItemsRepository itemsRepository)
        {
            _transactionsRepository = transactionsRepository;
            _itemsRepository = itemsRepository;
        }

        public async Task<IActionResult> Index()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var items = await _transactionsRepository.GetAllAsync();
            return View(items.Where(x => x.UserID == userId));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _transactionsRepository.Get(id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        public async Task<IActionResult> Create(string value)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["ItemID"] = new SelectList(await _itemsRepository.GetAllForUserID(userId), "ItemID", "ItemName");
            ViewBag.sum = value;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Transaction transaction)
      
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var values = new Transaction
                {
                    TransactionID = transaction.TransactionID,
                    ItemID = transaction.ItemID,
                    Price = transaction.Price,
                    TransactionDate = transaction.TransactionDate,
                    TransactionPlace = transaction.TransactionPlace,
                    UserID = userId
                };
                _transactionsRepository.Insert(values);
                return RedirectToAction(nameof(Index));
            }
            var userId2 = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["ItemID"] = new SelectList(await _itemsRepository.GetAllForUserID(userId2), "ItemID", "ItemName", transaction.ItemID);
            return View(transaction);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _transactionsRepository.Get(id);
            if (transaction == null)
            {
                return NotFound();
            }
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["ItemID"] = new SelectList(await _itemsRepository.GetAllForUserID(userId), "ItemID", "ItemName", transaction.ItemID);
            return View(transaction);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Transaction transaction)
        {
            if (id != transaction.TransactionID)
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
                        TransactionID = transaction.TransactionID,
                        ItemID = transaction.ItemID,
                        Price = transaction.Price,
                        TransactionDate = transaction.TransactionDate,
                        TransactionPlace = transaction.TransactionPlace,
                        UserID = userId
                    };
                    _transactionsRepository.Update(values);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_transactionsRepository.TransactionExists(transaction.TransactionID))
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
            ViewData["ItemID"] = new SelectList(await _itemsRepository.GetAllForUserID(userId2), "ItemID", "ItemName", transaction.ItemID);
            return View(transaction);
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _transactionsRepository.Get(id);
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
            var transaction = await _transactionsRepository.Get(id);
            _transactionsRepository.Delete(transaction);
            return RedirectToAction(nameof(Index));
        }
    }
}
