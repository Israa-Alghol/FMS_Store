using FMS_Store.Models;
using System.Collections.Generic;
using System.Linq;

namespace FMS_Store.Repositories
{
    public class CategoryDbRepository : IRepo<Category>
    {
        ProductDBContext db;

        public CategoryDbRepository(ProductDBContext _db)
        {
            db = _db;
        }
     



        public void Add(Category entity)
        {
            
            db.Categories.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
        }

        public Category Find(int id)
        {
            var category = db.Categories.SingleOrDefault(c => c.Id == id);
            return category;
        }

        public IList<Category> List()
        {
            return db.Categories.ToList();
        }



        public void Update(int id,Category newcategory)
        {
            db.Update(newcategory);
            db.SaveChanges();
        }

        public List<Category> Search (string term)
        {
            return db.Categories.Where(a => a.Type.Contains(term)).ToList();
        }


    }
    
}
