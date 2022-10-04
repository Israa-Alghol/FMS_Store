using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using FMS_Store.Repositories;
using FMS_Store.Models;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace FMS_Store.Tests.Mokcs
{
    internal class MockIRepository
    {
        public static Mock<IRepo<Product>> GetMock()
        {
            var mock = new Mock<IRepo<Product>>();

            var product = new List<Product>()
            {
                new Product()
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
                }
                
            };
            mock.Setup(m => m.List()).Returns(() => product);

            mock.Setup(m => m.Find(It.IsAny<int>()))
               .Returns((int id) => product.FirstOrDefault(o => o.Id == id));

            mock.Setup(m => m.Add(It.IsAny<Product>()))
               .Callback(() => { return; });

            mock.Setup(m => m.Update(It.IsAny<Product>()))
                   .Callback(() => { return; });

            return mock;
        }
        public static Mock<IRepo<Category>> GetMock2()
        {
            var mock = new Mock<IRepo<Category>>();
            return mock;
            
        }
        public static Mock<IWebHostEnvironment > GetMock3()
        {
            var mock = new Mock<IWebHostEnvironment>();
            return mock;
        }

        }
}
