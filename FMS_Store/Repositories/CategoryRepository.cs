using FMS_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FMS_Store
{
    public class CategoryRepository : IRepo<Category>
    {
        static List<Category> _categories = new List<Category>()
            {
                new Category() { Id = 1, Type = "Clothes"},
                new Category() { Id = 2, Type = "Houseware"},
                new Category() { Id = 3, Type = "Cosmetics"},
                new Category() { Id = 4, Type = "Electronics"},
                new Category() { Id = 5, Type = "Books"}
            };



        public void Add(Category entity)
        {
            entity.Id = _categories.Max(x => x.Id) + 1;
            _categories.Add(entity);    
        }

        public void Delete(int id)
        {
            var category = Find(id);
            _categories.Remove(category);
        }

        public Category Find(int id)
        {
            var category = _categories.SingleOrDefault(c => c.Id == id); 
            return category;
        }

        public IList<Category> List()
        {
            return _categories;
        }

      

        public void Update(Category newcategory)
        {
            var category = Find(newcategory.Id);
            category.Type = newcategory.Type;
            
        }




    }
}
