using FMS_Store.Models;
using System;
using System.Collections.Generic;

namespace FMS_Store
{
    public interface IRepo<TEntity>
    {
        IList<TEntity> List();
        IList<TEntity> List(Func<Product, bool> filter);
        TEntity Find (int id);
        void Add (TEntity entity);
        void Delete (int id);   
        void Update (TEntity entity);
        List<TEntity> Search(string term);
    }
}
