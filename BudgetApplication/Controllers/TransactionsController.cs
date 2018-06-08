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
using BudgetApplication.Repository;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;

namespace BudgetApplication.Controllers
{
    [Authorize(Roles = "Administrator, User")]
    public class TransactionsController : Controller
    {
        private readonly ITransactionsRepository _transactionsRepository;
        private readonly IItemsRepository _itemsRepository;
        private readonly IHostingEnvironment _environment;


        public TransactionsController(ITransactionsRepository transactionsRepository, IItemsRepository itemsRepository, IHostingEnvironment IHostingEnvironment)
        {
            _transactionsRepository = transactionsRepository;
            _itemsRepository = itemsRepository;
            _environment = IHostingEnvironment;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var items = await _transactionsRepository.GetAllAsync();
            return View(items.Where(x => x.UserID == userId));
        }

        // GET: Transactions/Details/5
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

        // GET: Transactions/Create
        public async Task<IActionResult> Create(string value)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["ItemID"] = new SelectList(await _itemsRepository.GetAllForUserID(userId), "ItemID", "ItemName");
            ViewBag.sum = value;
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

        // GET: Transactions/Edit/5
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

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Transactions/Delete/5
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
        /*
        [HttpPost]
        public IActionResult Index(string name)
        {
            var newFileName = string.Empty;
            var fileName = string.Empty;

            if (HttpContext.Request.Form.Files != null)
            {
                //   var fileName = string.Empty;
                string PathDB = string.Empty;

                var files = HttpContext.Request.Form.Files;

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        //Getting FileName
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var FileExtension = Path.GetExtension(fileName);

                        // concating  FileName + FileExtension
                        newFileName = myUniqueFileName + FileExtension;

                        // Combines two strings into a path.
                        fileName = Path.Combine(_environment.WebRootPath, "scans") + $@"\{newFileName}";

                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }

                    }
                }
            }
            Tesseract tess = new Tesseract(fileName);
            string text = tess.getText();
            Regex word = new Regex(@"(SUMA|SUMA PLN|Suma PLN).*?([0-9,]+)");
            Match m = word.Match(text);
            string sum = m.Value;
            ViewBag.Sum = sum;
            ViewBag.Message = text;
            System.IO.File.Delete(fileName);
            return PartialView("~/views/Transactions/Scanner.cshtml");
        }

      */



    }
}
