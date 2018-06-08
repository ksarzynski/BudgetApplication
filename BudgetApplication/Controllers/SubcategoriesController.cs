using Microsoft.AspNetCore.Mvc;
using BudgetApplication.Models;
using Microsoft.AspNetCore.Authorization;
using BudgetApplication.Repository;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BudgetApplication.Controllers
{
    [Authorize(Roles = "Administrator, User")]
    public class SubcategoriesController : Controller
    {
        private readonly ISubcategoriesRepository _subcategoriesRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        public SubcategoriesController(ISubcategoriesRepository subcategoriesRepository, ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
            _subcategoriesRepository = subcategoriesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var subcategories = await _subcategoriesRepository.GetAllAsync();
            return View(subcategories.Where(x => x.UserID == userId));
        }

        public async Task<IActionResult> Details(int id)
        {
            var item =  await _subcategoriesRepository.Get(id);
            if (item == null) return NotFound();

            return View(item);
        }
        public async Task<IActionResult> Create()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["CategoryID"] = new SelectList(await _categoriesRepository.GetAllForUserID(userId), "CategoryID", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( Subcategory subcategory)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                subcategory.UserID = userId;

                _subcategoriesRepository.Insert(subcategory);
                ViewData["CategoryID"] = new SelectList(await _categoriesRepository.GetAllForUserID(userId), "CategoryID", "CategoryName", subcategory.CategoryID);
                return RedirectToAction(nameof(Index));
            }
            return View(subcategory);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcategory = await _subcategoriesRepository.Get(id);
            if (subcategory == null)
            {
                return NotFound();
            }
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["CategoryID"] = new SelectList(await _categoriesRepository.GetAllForUserID(userId), "CategoryID", "CategoryName", subcategory.CategoryID);
            return View(subcategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Subcategory subcategory)
        {
            if (id != subcategory.SubcategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid && _subcategoriesRepository.SubcategoryExists(subcategory.SubcategoryID))
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var values = new Subcategory
                {
                    SubcategoryID = subcategory.SubcategoryID,
                    CategoryID = subcategory.CategoryID,
                    SubcategoryName = subcategory.SubcategoryName,
                    UserID = userId

                };
                _subcategoriesRepository.Update(values);
                return RedirectToAction(nameof(Index));
            }
            var userId2 = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["CategoryID"] = new SelectList(await _categoriesRepository.GetAllForUserID(userId2), "CategoryID", "CategoryName", subcategory.CategoryID);
            return View(subcategory);
        }
      
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Subcategory subcategory = await _subcategoriesRepository.Get(id);
            if (subcategory == null)
            {
                return BadRequest();
            }
            return View(subcategory);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Subcategory subcategory = await _subcategoriesRepository.Get(id);
            if (subcategory == null)
            {
                return BadRequest();
            }
            _subcategoriesRepository.Delete(subcategory);
            return RedirectToAction(nameof(Index));
        }
    }
}
