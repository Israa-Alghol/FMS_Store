using FMS_Store.Models;
using FMS_Store.Repositories;
using FMS_Store.ViewModels;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FMS_Store.Tests.Repository
{
    public class ProductRepositoryTest
    {
       
        //private ProductDbRepository repository;
        //private CategoryDbRepository repository2;

        //private DbContextOptions<ProductDBContext> dbContextOptions;

        //protected readonly ProductApiControllerTest db;
        

        //public ProductRepositoryTest(ProductApiControllerTest _db)
        //{
        //    db = _db;
            

        //}

        [Fact]
        public void Task_AddProduct_ReturnProduct()
        {
            // Arrange
            IRepo<Product> sut = GetInMemoryPersonRepository();
            Product product = new Product()
            {
                    Id = 3,
                    Name = "JTA",
                    Price = 12000,
                    Description = "for child",
                    ImageUrl = "",
                    CategoryId = 1,
                    Category = new Category
                    {

                        Id = 1,
                        Type = "Game",

                    }
            };

            //Action
            sut.Add(product);

            //Assert
            Assert.Equal(3, product.Id);
            Assert.Equal("JTA", product.Name);
            Assert.Equal(12000, product.Price);
            Assert.Equal("for child", product.Description);
            Assert.Equal("", product.ImageUrl);
            Assert.Equal(1, product.CategoryId);
            Assert.Equal(1, product.Category.Id);
            Assert.Equal("Game", product.Category.Type);

            //Mock<IRepo<Product>> repo = new Mock<IRepo<Product>>();

            //repo.Setup(x => x.Add(It.IsAny<Product>())).Returns(products);
            //Product product = new Product(repo.Object);

        }
        private IRepo<Product> GetInMemoryPersonRepository()
        {
            DbContextOptions<ProductDBContext> options;
            var builder = new DbContextOptionsBuilder<ProductDBContext>();
            builder.UseInMemoryDatabase("DB");
            options = builder.Options;
            ProductDBContext dbContext = new ProductDBContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            return new ProductDbRepository(dbContext);
        }
    }
}
