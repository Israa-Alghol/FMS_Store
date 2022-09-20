using FMS_Store.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace FMS_Store
{
    public class ProductRepostiory : IRepo<Product>
    {

        static List<Product> _products = new List<Product>()
        {


                new Product { Id = 1,
                    Name = "washing machine",
                    Price = 3000000,
                    Description =" White",
                    ImageUrl = "washing machine.JPG",
                    Category = new Category {Id = 4} },
                new Product { Id = 2,
                    Name = "Big Data Applications and Use Cases",
                    Price = 5000,
                    Description ="220 pages",
                    ImageUrl = "Book.jpg",
                    Category = new Category {Id = 5} }

        };

        public void Add(Product entity)
        {
            entity.Id = _products.Max(x => x.Id)+1;
            _products.Add(entity);  
        }

        public void Delete(int id)
        {
            var product = Find(id);
            _products.Remove(product);
        }

        public Product Find(int id)
        {
            var product = _products.SingleOrDefault(p => p.Id == id);
            return product;
        }

        public IList<Product> List()
        {
            return _products;
        }

     

        public void Update(Product newProduct)
        {
            var product = Find(newProduct.Id);
            product.Name = newProduct.Name;
            product.Price = newProduct.Price;
            product.Description = newProduct.Description;
            product.Category = newProduct.Category;
            product.ImageUrl = newProduct.ImageUrl;
        }
    }
}
