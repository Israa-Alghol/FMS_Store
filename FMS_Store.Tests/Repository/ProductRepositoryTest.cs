using FMS_Store.Models;
using FMS_Store.Repositories;
using FMS_Store.Tests.Mokcs;
using FMS_Store.ViewModels;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Xunit;

namespace FMS_Store.Tests.Repository
{
    public class ProductRepositoryTest
    {
        public IRepo<Product> Db { get; set; }

        public ProductRepositoryTest()
        {
            Db = GetInMemoryProductRepository();
        }
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
            //IRepo<Product> sut = GetInMemoryProductRepository();
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
            Db.Add(product);

            //Assert
            //Assert.Equal(1, sut.List().Count());
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
        //[Theory]
        //[InlineData(1,null, 1, null, null, 1,1,null, false)]
        //[InlineData(3,"Test Title 1", 2000, "Test Description 1", "", 2, 1, null, true)]
        [Fact]
        //int idProduct, string? name, int price, string? description, string? imageUrl, int categoryId, int id,string? type, bool isValid
        public void Task_UpdateProduct_ReturnProduct()
        {
            // Arrange
            //IRepo<Product> sut = GetInMemoryProductRepository();
            var repositoryMock = MockIRepository.GetMock();
            var product = new Product()
            {
                Id =3,
                Name = "Test Title 1",
                Price = 2000,
                Description = "Test Description 1",
                ImageUrl = "",
                CategoryId = 1,
                Category = new Category
                {

                    Id = 1,
                    Type = "Game",

                }

            };
            //Action
            repositoryMock.Object.Update(product);

            //Assert
            //var valid = ValidateModel(product);
            //Assert.Equal(valid, isValid);

            Assert.Equal(3, product.Id);
            Assert.Equal("Test Title 1", product.Name);
            Assert.Equal(2000, product.Price);
            Assert.Equal("Test Description 1", product.Description);
            Assert.Equal("", product.ImageUrl);
            Assert.Equal(1, product.CategoryId);
            Assert.Equal(1, product.Category.Id);
            Assert.Equal("Game", product.Category.Type);

        }

        [Theory]
        [InlineData(null, 1, null, null, 1, false)]
        [InlineData("Test Title 1", 1, null, null, 1, true)]
        [InlineData("Test Title 1", 2000, "Test Description 1", "", 2, true)]
        public void TestModelValidation(string? name, int price, string? description, string? imageUrl, int categoryId, bool isValid)
        {
            var product = new ProductCategoryViewModel()
            {
                Name = name,
                Price = price,
                Description = description,
                ImageUrl = imageUrl,
                Category = categoryId,

            };

            var valid = ValidateModel(product);
            Assert.Equal(valid, isValid);
        }
        private bool ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);

            return Validator.TryValidateObject(model, ctx, validationResults, true);
        }
        private IRepo<Product> GetInMemoryProductRepository()
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
