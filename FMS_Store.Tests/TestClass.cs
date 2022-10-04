using FMS_Store.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS_Store.Tests
{
    public class TestClass
    {
        public int ProductId { get; set; }


        [Required]
        [MaxLength(100)]
        // [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        public string Description { get; set; }

        public int Category { get; set; }

        public List<Category> Categories { get; set; }

        public IFormFile File { get; set; }
        public string ImageUrl { get; set; }
    }
}
