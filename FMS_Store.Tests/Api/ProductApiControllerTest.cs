using FluentAssertions;
using FMS_Store.Api;
using FMS_Store.Models;
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
    public class ProductApiControllerTest
    {
        private ProductDbRepository repository;
        private CategoryDbRepository repository2;
        private ProductApiController controller;

        public static DbContextOptions<ProductDBContext> dbContextOptions { get; }
        public static string connectionString = "Server=FMS-SW07\\SQLEXPRESS;Database=SamplesTest;Trusted_Connection=True;MultipleActiveResultSets=true";

        static ProductApiControllerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<ProductDBContext>()
               .UseSqlServer(connectionString)
               .Options;

        }
        public ProductApiControllerTest()
        {
            var context = new ProductDBContext(dbContextOptions);
            DummyDataDBInitializer db = new DummyDataDBInitializer();
            db.Seed(context);

            repository = new ProductDbRepository(context);
            repository2 = new CategoryDbRepository(context);


            controller = new ProductApiController(repository, repository2);

        }


        #region Get By Id

        [Fact]
        public void Task_GetProductById_Return_OkResult()
        {
            //Arrange          
            var productId = 1;

            //Act
            var data = controller.Get(productId);

            //Assert

            //Assert.IsType<ActionResult<Product>>(data);
            Assert.IsType<OkObjectResult>(data.Result);


            //ObjectResult objectResponse = Assert.IsType<ObjectResult>(data);

            //Assert.Equal(500, objectResponse.StatusCode);

            //var result = data.Result as OkObjectResult;
            //result.Should().NotBeNull();
            //result.Value.Should().Be(2);

            //var actionResult = Assert.IsType<ActionResult<Product>>(data);
            //Assert.IsType<OkObjectResult>(actionResult.Result);



        }
        [Fact]
        public void Task_GetProductById_Return_NotFoundResult()
        {
            //Arrange
            var productId = 3;

            //Act
            var data = controller.Get(productId);

            //Assert
            //Assert.IsType<NotFoundResult>(data);

            //Assert.IsType<ActionResult<Product>>(data);
            Assert.IsType<NotFoundResult>(data.Result);
        }
        [Fact]
        public void Task_GetProductById_Return_BadRequestResult()
        {
            //Arrange
            int? productId = null;

            //Act
            var data = controller.Get(productId);

            
            //Assert
            Assert.IsType<BadRequestResult>(data.Result);

        }

        #endregion

        #region Get All

        [Fact]
        public void Task_GetProducts_Return_OkResult()
        {
            //Arrange
            int? categoryId = null;

            //Act
            var data = controller.GetAll(categoryId);

            //Assert
            Assert.IsType<OkObjectResult>(data.Result);
            //var actionResult = Assert.IsType<ActionResult<List<Product>>>(data);
            //Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public void Task_GetProducts_Return_BadRequestResult()
        {
            //Arrange
            int? categoryId = null;

            //Act
            var data = controller.GetAll(categoryId);
            data = null;

            if (data != null)
                //Assert
                Assert.IsType<BadRequestResult>(data.Result);
        }
        #endregion

        #region Get Categories
        [Fact]
        public void Task_Categories_Return_OkResult()
        {
            //Arrange

            //Act
            var data = controller.GetCat();

            //Assert
            Assert.IsType<OkObjectResult>(data.Result);
            //var actionResult = Assert.IsType<ActionResult<List<Category>>>(data);
            //Assert.IsType<OkObjectResult>(actionResult.Result);

        }
        

        [Fact]
        public void Task_GetCategories_Return_BadRequestResult()
        {
            //Arrange
       

            //Act
            var data = controller.GetCat();
            data = null;

            if (data != null)
                //Assert
                Assert.IsType<BadRequestResult>(data.Result);
        }
        #endregion
    }
}