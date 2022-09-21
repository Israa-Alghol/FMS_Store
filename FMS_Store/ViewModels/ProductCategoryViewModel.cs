using FMS_Store.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS_Store.ViewModels
{
    public class ProductCategoryViewModel
    {
        public int ProductId { get; set; }


        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        // [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }  

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public List<Category> Categories { get; set; }

        public IFormFile File { get; set; }
        public string ImageUrl { get; set; }
    }
}
 