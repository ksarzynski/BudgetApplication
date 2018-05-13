using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BudgetApplication.Models;
using Microsoft.AspNetCore.Authorization;
using BudgetApplication.Repository;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace BudgetApplication.Controllers
{
    [Authorize(Roles = "Administrator, User")]
    public class ItemsController : Controller
    {
        private readonly IItemsRepository _itemsRepository;
        private readonly ISubcategoriesRepository _subcategoriesRepository;

        public ItemsController(IItemsRepository itemsRepository, ISubcategoriesRepository subcategoriesRepository)
        {
            _itemsRepository = itemsRepository;
            _subcategoriesRepository = subcategoriesRepository ?? throw new System.ArgumentNullException(nameof(subcategoriesRepository));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var items = await _itemsRepository.GetAllAsync();
            return View(items.Where(x=> x.UserID == userId));
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _itemsRepository.Get(id);
            if (item != null)
            {
                return View(item);
            }
            
            return NotFound();
        }
        public async Task<IActionResult> Create()
        {
            ViewData["SubcategoryID"] = new SelectList(await _subcategoriesRepository.GetAllAsync(), "SubcategoryID", "SubcategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItemName")] Item item)
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                item.UserID = userId;
                ViewData["SubcategoryID"] = new SelectList(await _subcategoriesRepository.GetAllAsync(), "SubcategoryID", "SubcategoryName");
                _itemsRepository.Insert(item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            Item item = await _itemsRepository.Get(id);
            if (id != item.ItemID)
            {
                return NotFound();
            }
            ViewData["SubcategoryID"] = new SelectList(await _subcategoriesRepository.GetAllAsync(), "SubcategoryID", "SubcategoryName");
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,ItemName, UserID")] Item item)
        {
            if (item == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                item.UserID = userId;
                ViewData["SubcategoryID"] = new SelectList(await _subcategoriesRepository.GetAllAsync(), "SubcategoryID", "SubcategoryName");
                _itemsRepository.Update(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Item item = await _itemsRepository.Get(id);
            if (item == null)
            {
                return BadRequest();
            }
            return View(item);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Item item = await _itemsRepository.Get(id);
            if (item == null)
            {
                return BadRequest();
            }
            _itemsRepository.Delete(item);
            return RedirectToAction("Index");
        }
    }
}
