using FluentAssertions;
using FMS_Store.Api;
using FMS_Store.Repositories;
using FMS_Store.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FMS_Store.Tests.Repository
{
    public class ProductRepositoryTest
    {
        private ProductDbRepository repository;
        private ProductDbRepository repository2;
        public static DbContextOptions<ProductDBContext> dbContextOptions { get; }
        public static string connectionString = "Server=ABCD;Database=Samples;Trusted_Connection=True;MultipleActiveResultSets=true";

        static ProductRepositoryTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<ProductDBContext>()
               .UseSqlServer(connectionString)
               .Options;



        }
        public ProductRepositoryTest()
        {
            var context = new ProductDBContext(dbContextOptions);
            DummyDataDBInitializer db = new DummyDataDBInitializer();
            db.Seed(context);

            repository = new ProductDbRepository(context);

        }


        #region Get By Id

        [Fact]
        public void Task_GetPostById_Return_OkResult()
        {
            //Arrange
            var controller = new ProductApiController(repository, (IRepo<Models.Category>)repository2);
            var postId = 2;

            //Act
            var data = controller.Get(postId);

            //Assert
            Assert.IsType<OkObjectResult>(data);

        }
        [Fact]
        public void Task_GetPostById_Return_NotFoundResult()
        {
            //Arrange
            var controller = new ProductApiController(repository, (IRepo<Models.Category>)repository2);
            var postId = 3;

            //Act
            var data = controller.Get(postId);

            //Assert
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public void Task_GetPostById_MatchResult()
        {
            //Arrange
            var controller = new ProductApiController(repository, (IRepo<Models.Category>)repository2);
            int? postId = 1;

            //Act
            var data = controller.Get((int)postId);

            //Assert
            Assert.IsType<OkObjectResult>(data);

            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var product = okResult.Value.Should().BeAssignableTo<ProductCategoryViewModel>().Subject;

            Assert.Equal("Test Title 1", product.Name);
            Assert.Equal("Test Description 1", product.Description);
        }

        #endregion
    }
}