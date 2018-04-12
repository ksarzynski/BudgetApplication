using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BudgetApplication.Models;
using Microsoft.AspNetCore.Authorization;
using BudgetApplication.Repository;

namespace BudgetApplication.Controllers
{
    [Authorize(Roles = "Administrator, User")]
    public class CategoriesController : Controller
    {
        private readonly IRepository<Category> _categoriesRepository;
        public CategoriesController(IRepository<Category> context) => _categoriesRepository = context;

        [HttpGet]
        public IActionResult Index()
        {
            return View(_categoriesRepository.GetAll().AsEnumerable());
        }

        // GET: Products/Details/5
        public IActionResult Details(int id)
        {
            var product = _categoriesRepository.Get(id);
            if (product == null) return NotFound();

            return View(product);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                _categoriesRepository.Insert(category);
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public ActionResult Edit(int? id)
        {
            Category category = _categoriesRepository.Get(id);
            if (id != category.Id)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,CategoryName")] Category product)
        {
            if (product == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _categoriesRepository.Update(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // POST: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Category category = _categoriesRepository.Get(id);
            if (category == null)
            {
                return BadRequest();
            }
            return View(category);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Category category = _categoriesRepository.Get(id);
            if (category == null)
            {
                return BadRequest();
            }
            _categoriesRepository.Delete(category);
            return RedirectToAction("Index");
        }
    }
}
