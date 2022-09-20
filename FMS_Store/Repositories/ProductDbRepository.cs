using FMS_Store.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FMS_Store.Repositories
{
    public class ProductDbRepository : IRepo<Product>
    {
        ProductDBContext db;

        public ProductDbRepository(ProductDBContext _db)
        {
            db = _db;
        }




        public void Add(Product entity)
        {
            
            db.Products.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
        }

        public Product Find(int id)
        {
            var product = db.Products.SingleOrDefault(p => p.Id == id);
            return product;
        }

        public IList<Product> List()
        {
            return db.Products.Include(a => a.Category).ToList();
        }



        public void Update(Product newProduct)
        {
            db.Update(newProduct);
            db.SaveChanges();
        }
    }
    
}
