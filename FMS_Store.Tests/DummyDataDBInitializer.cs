using FMS_Store.Models;
using System;
using Xunit;

namespace FMS_Store.Tests
{
    public class DummyDataDBInitializer
    {
        public DummyDataDBInitializer()
        {
        }

        public void Seed(ProductDBContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();


            context.Categories.AddRange(
                new Category() { Type = "Games" },
                new Category() { Type = "Sports' Wear" },
                new Category() { Type = "Plants" }
            );

            context.Products.AddRange(
            new Product() { Name = "Test Title 1", Price = 2000, Description = "Test Description 1", ImageUrl = "", CategoryId = 2 },
            new Product() { Name = "Test Title 2", Price = 3000, Description = "Test Description 2", ImageUrl = "", CategoryId = 3 }
            );
            context.SaveChanges();

        }
    }
}
