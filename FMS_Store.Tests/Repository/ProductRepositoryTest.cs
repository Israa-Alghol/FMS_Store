using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using FMS_Store.Repositories;
using Moq;
using FMS_Store.Tests.Mokcs;
using System.ComponentModel.DataAnnotations;
using FMS_Store.Models;
using Microsoft.AspNetCore.Mvc;
using FMS_Store.Controllers;
using System.Net;
using FMS_Store.ViewModels;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace FMS_Store.Tests.Api
{
    public class MockRepositoryWrapper
    {

        
        private bool ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);

            return Validator.TryValidateObject(model, ctx, validationResults, true);
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
            Assert.Equal(valid,isValid);
        }
        [Fact]
        public void WhenGettingAllProducts_ThenAllProductsReturn()
        {
            //Arrange
            var repositoryMock = MockIRepository.GetMock();
            var repositoryMock2 = MockIRepository.GetMock2();
            var repositoryMock3 = MockIRepository.GetMock3();
            var controller = new ProductController(repositoryMock.Object, repositoryMock2.Object, repositoryMock3.Object);

            //Act
            var result = controller.Index() as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<Product>>(result.Value);
            Assert.NotEmpty(result.Value as IEnumerable<Product>);
        }
        [Fact]
        public void GivenValidRequest_WhenCreatingProduct_ThenCreatedReturns()
        {
            //Arrange
            var repositoryMock = MockIRepository.GetMock();
            var repositoryMock2 = MockIRepository.GetMock2();
            //var repositoryMock2 = new CategoryDbRepository();
            var repositoryMock3 = MockIRepository.GetMock3();
            var controller = new ProductController(repositoryMock.Object, repositoryMock2.Object, repositoryMock3.Object);

            //HttpClient client = new HttpClient();
            var product = new ProductCategoryViewModel()
            {
                ProductId = 1,
                Name = "TestName",
                Price = 1000,
                Description = "TestDescription",                
                Category = 1,
                Categories = new List<Category>()
                {
                    new Category()
                    {
                        Id = 1,
                        Type = "Test",
                        
                    }
                },
                ImageUrl = "",


            };

            //client.PostAsync("", );

            //Act
            var result = controller.Create(product) as ObjectResult;
            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<CreatedAtRouteResult>(result);
            Assert.Equal((int)HttpStatusCode.Created, result!.StatusCode);
            Assert.Equal("Create", (result as CreatedAtRouteResult)!.RouteName);
        }
       
    }
}
