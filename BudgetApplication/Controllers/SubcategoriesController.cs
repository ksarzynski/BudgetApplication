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
using Microsoft.AspNetCore.Authorization;
using BudgetApplication.Repository;

namespace BudgetApplication.Controllers
{
    [Authorize(Roles = "Administrator, User")]
    public class SubcategoriesController : Controller
    {
        private readonly IRepository<Subcategory> _subcategoriesRepository;
        public SubcategoriesController(IRepository<Subcategory> context) => _subcategoriesRepository = context;

        [HttpGet]
        public IActionResult Index()
        {
            return View(_subcategoriesRepository.GetAll().AsEnumerable());
        }

        // GET: Products/Details/5
        public IActionResult Details(int id)
        {
            var item = _subcategoriesRepository.Get(id);
            if (item == null) return NotFound();

            return View(item);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,SubcategoryName")] Subcategory subcategory)
        {
            if (ModelState.IsValid)
            {
                _subcategoriesRepository.Insert(subcategory);
                return RedirectToAction("Index");
            }

            return View(subcategory);
        }

        public ActionResult Edit(int? id)
        {
            Subcategory subcategory = _subcategoriesRepository.Get(id);
            if (id != subcategory.Id)
            {
                return NotFound();
            }
            return View(subcategory);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,SubcategoryName")] Subcategory subcategory)
        {
            if (subcategory == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _subcategoriesRepository.Update(subcategory);
                return RedirectToAction("Index");
            }
            return View(subcategory);
        }

        // POST: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Subcategory subcategory = _subcategoriesRepository.Get(id);
            if (subcategory == null)
            {
                return BadRequest();
            }
            return View(subcategory);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Subcategory subcategory = _subcategoriesRepository.Get(id);
            if (subcategory == null)
            {
                return BadRequest();
            }
            _subcategoriesRepository.Delete(subcategory);
            return RedirectToAction("Index");
        }
    }
}
