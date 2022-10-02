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
        [InlineData("Test Title 1", 2000, "Test Description 1", "", 2, true)]
        public void TestModelValidation(string? name, int price, string? description, string? imageUrl, int categoryId, bool isValid)
        {
            var product = new Product()
            {
                Name = name,
                Price = price,
                Description = description,
                ImageUrl = imageUrl,
                CategoryId = categoryId,
              
            };

            var valid = ValidateModel(product);
            Assert.Equal(isValid, valid);
        }
        //[Fact]
        //public void GivenValidRequest_WhenCreatingProduct_ThenCreatedReturns()
        //{
    

            
        //    var product = new ProductCategoryViewModel()
        //    {
        //        Name = "TestName",
        //        Price = 1000,
        //        Description = "TestDescription",
        //        ImageUrl = "",
        //        Category = 1,
                
        //    };
        //    var result = ProductController.Create(product) as ObjectResult;
        //    Assert.NotNull(result);
        //    Assert.IsAssignableFrom<CreatedAtRouteResult>(result);
        //    Assert.Equal((int)HttpStatusCode.Created, result!.StatusCode);
        //    Assert.Equal("Find", (result as CreatedAtRouteResult)!.RouteName);
        //}
    }
}
