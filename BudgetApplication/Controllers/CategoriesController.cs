using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BudgetApplication.Models;
using Microsoft.AspNetCore.Authorization;
using BudgetApplication.Repository;
using System.Threading.Tasks;
using System.Security.Claims;

namespace BudgetApplication.Controllers
{
    [Authorize(Roles = "User")]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public CategoriesController(ICategoriesRepository context) => _categoriesRepository = context;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var categories = await _categoriesRepository.GetAllForUserID(userId);
            return View(categories);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _categoriesRepository.Get(id);
            if (product == null) return NotFound();

            return View(product);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,CategoryName,UserID")] Category category)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                category.UserID = userId;

                _categoriesRepository.Insert(category);
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoriesRepository.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Category category)
        {
            if (id != category.CategoryID) return NotFound();

            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var values = new Category
                {
                    CategoryID = category.CategoryID,
                    CategoryName = category.CategoryName,
                    UserID = userId
                };
                _categoriesRepository.Update(values);
                return RedirectToAction(nameof(Index));
            }
                
            return View(category);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Category category = await _categoriesRepository.Get(id);
            if (category == null)
            {
                return BadRequest();
            }
            return View(category);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Category category = await _categoriesRepository.Get(id);
            if (category == null)
            {
                return BadRequest();
            }
            _categoriesRepository.Delete(category);
            return RedirectToAction("Index");
        }

        public bool CategoryExists(int id)
        {
            return _categoriesRepository.CategoryExists(id);
        }
               
    }
}
