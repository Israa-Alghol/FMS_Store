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
                    string fileName = UploadFile(model.File) ?? string.Empty;                    

                    if (model.Category == -1)
                    {
                        ViewBag.Message = "Please select an category from the list!";

                        return View(GetAllCategories());
                    }

                    var category = categoryRepository.Find(model.Category);
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
                Category = categoryId,
                //CategoryId = product.Category.Id,
                Categories = categoryRepository.List().ToList(),
                ImageUrl = product.ImageUrl
            };
            return View(viewModel);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategoryViewModel viewModel)
        {
            try
            {

                //string fileName = string.Empty;
                //string fileName = "";
                //if (viewModel.File == null)
                //{
                //    fileName = productRepository.Find(viewModel.ProductId).ImageUrl;
                //}
                //else
                //    fileName = UploadFile(viewModel.File, viewModel.ImageUrl);

                string fileName = UploadFile(viewModel.File, viewModel.ImageUrl) ?? string.Empty;

                var category = categoryRepository.Find(viewModel.Category);
                Product product = new Product
                {
                    Id=viewModel.ProductId,
                    Name = viewModel.Name,
                    Price = viewModel.Price,
                    Description = viewModel.Description,
                    Category = category,
                    ImageUrl = fileName
                };
                productRepository.Update(product);
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
        string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string Uploads = Path.Combine(hosting.WebRootPath, "Uploads");            
                string fullPath = Path.Combine(Uploads, file.FileName);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                return file.FileName;
            }
            return null;
        }
        string UploadFile(IFormFile file, string imageUrl)
        {
           if (file != null)
            {
                string Uploads = Path.Combine(hosting.WebRootPath, "Uploads");
                string newPath = Path.Combine(Uploads, file.FileName);
                //Delete the old file
                string OldPath = Path.Combine(Uploads, imageUrl);

                if (OldPath != newPath)
                {
                    System.IO.File.Delete(OldPath);
                    //Save the new file
                    using (var fileStream = new FileStream(newPath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
                return file.FileName;
            }
            return imageUrl;    
        }
        public ActionResult Search (string term)
        {
            var result = productRepository.Search(term);

            return View("Index",result);    
        }
    }
}
