using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BudgetApplication.Models;
using Microsoft.AspNetCore.Authorization;
using BudgetApplication.Repository;

namespace BudgetApplication.Controllers
{
    [Authorize(Roles = "Administrator, User")]
    public class ItemsController : Controller
    {
        private readonly IRepository<Item> _itemsRepository;
        public ItemsController(IRepository<Item> context) => _itemsRepository = context;

        [HttpGet]
        public IActionResult Index()
        {
            return View(_itemsRepository.GetAll().AsEnumerable());
        }

        // GET: Products/Details/5
        public IActionResult Details(int id)
        {
            var item = _itemsRepository.Get(id);
            if (item == null) return NotFound();

            return View(item);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,ItemName")] Item item)
        {
            if (ModelState.IsValid)
            {
                _itemsRepository.Insert(item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        public ActionResult Edit(int? id)
        {
            Item item = _itemsRepository.Get(id);
            if (id != item.Id)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,ItemName")] Item item)
        {
            if (item == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _itemsRepository.Update(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // POST: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Item item = _itemsRepository.Get(id);
            if (item == null)
            {
                return BadRequest();
            }
            return View(item);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Item item = _itemsRepository.Get(id);
            if (item == null)
            {
                return BadRequest();
            }
            _itemsRepository.Delete(item);
            return RedirectToAction("Index");
        }
    }
}
