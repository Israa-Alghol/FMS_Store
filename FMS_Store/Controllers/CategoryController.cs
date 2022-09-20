using FMS_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FMS_Store.Controllers
{
    public class CategoryController : Controller
    {

        private readonly IRepo<Category> categoryRepository;
        public CategoryController(IRepo<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        // GET: CategoryController
        [HttpGet]
        public ActionResult Index()
        {
            var category = categoryRepository.List();
            return View(category);
        }


        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            var category = categoryRepository.Find(id);
            return View(category);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            //var highestId = categoryRepository.Any() ? categoryRepository.Max(x => x.Id) : 1;
            //category.Id = highestId + 1;

            //if (category.Name != null)
            //{
            //    categoryRepository.Add(category);

            //}
            //else
            //{
            //    return RedirectToAction(nameof(Create));
            //}

            //return RedirectToAction(nameof(Index));
            try
            {
                categoryRepository.Add(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var category = categoryRepository.Find(id);
            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,Category category)
        {
            try
            {
                categoryRepository.Update(id, category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var category = categoryRepository.Find(id);
            return View(category);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category category)
        {
            try
            {
                categoryRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
