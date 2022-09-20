using FMS_Store.Models;
using FMS_Store.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FMS_Store.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepo<Product> productRepository;
        private readonly IRepo<Category> categoryRepository;
        private readonly IWebHostEnvironment hosting;

        public ProductController(IRepo<Product> productRepository, 
            IRepo<Category> categoryRepository,
            IWebHostEnvironment hosting )
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.hosting = hosting;
        }

        // GET: ProductController
        [HttpGet]
        public ActionResult Index()
        {
            var product = productRepository.List();
            return View(product);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var product = productRepository.Find(id);
            return View(product);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            var model = new ProductCategoryViewModel
            {

                Categories = FillSelectList()
            };
            return View(model);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = string.Empty;
                    if(model.File != null)
                    {
                        string Uploads = Path.Combine(hosting.WebRootPath, "Uploads");
                        fileName = model.File.FileName;
                        string fullPath = Path.Combine(Uploads,fileName);
                        using (var fileStream = new FileStream(fullPath, FileMode.Create))
                        {
                            model.File.CopyTo(fileStream);
                        }
                    }

                    if (model.CategoryId == -1)
                    {
                        ViewBag.Message = "Please select an category from the list!";

                        return View(GetAllCategories());
                    }

                    var category = categoryRepository.Find(model.CategoryId);
                    Product product = new Product
                    {
                        Id = model.ProductId,
                        Name = model.Name,
                        Price = model.Price,
                        Description = model.Description,
                        Category = category,
                        ImageUrl = fileName
                    };
                    productRepository.Add(product);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
           
            ModelState.AddModelError("","You have to fill all the required fields!");
            return View(GetAllCategories());
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = productRepository.Find(id);
            //var categoryId = 
            // product.Category == null ? product.Category.Id = 0 : product.Category.Id;

      
            var categoryId = 0;
            if (product.Category == null)
            {

                categoryId = product.Id;
                product.Category.Id = 0;
            }
            else

                categoryId = product.Category.Id;


            var viewModel = new ProductCategoryViewModel
            {
                ProductId = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = categoryId,
                //CategoryId = product.Category.Id,
                Categories = categoryRepository.List().ToList(),
                ImageUrl = product.ImageUrl
            };
            return View(viewModel);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,ProductCategoryViewModel viewModel)
        {
            try
            {

                //string fileName = string.Empty;
                string fileName = "";
                if (fileName == null)
                {
                    fileName = string.Empty;
                }
                else
                //fileName = Path.GetFileName(fileName);
                    fileName= viewModel.ImageUrl;


                if (viewModel.File != null)
                {
                    string Uploads = Path.Combine(hosting.WebRootPath, "Uploads");
                    fileName = viewModel.File.FileName;
                    string fullPath = Path.Combine(Uploads, fileName);
                    //Delete the old file
                    string oldFileName = viewModel.ImageUrl;
                    string fullOldPath = Path.Combine(Uploads ,oldFileName);

                    if(fullPath != fullOldPath)
                    {
                        System.IO.File.Delete(fullOldPath);
                        //Save the new file
                        using (var fileStream = new FileStream(fullPath, FileMode.Create))
                        {
                            viewModel.File.CopyTo(fileStream);
                        }
                    }
                }

                var category = categoryRepository.Find(viewModel.CategoryId);
                Product product = new Product
                {
                    Id=viewModel.ProductId,
                    Name = viewModel.Name,
                    Price = viewModel.Price,
                    Description = viewModel.Description,
                    Category = category,
                    ImageUrl = fileName
                };
                productRepository.Update(viewModel.ProductId, product);
                return RedirectToAction(nameof(Index));
                
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            var product = productRepository.Find(id);

            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                productRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        List<Category> FillSelectList()
        {
            var categories = categoryRepository.List().ToList();
            categories.Insert(0, new Category { Id = -1, Type = " --- Please select an category --- " });
            return categories;
        }
        ProductCategoryViewModel GetAllCategories ()
        {
            var vmodel = new ProductCategoryViewModel
            {

                Categories = FillSelectList()
            };
            return vmodel;
        }
    }
}
