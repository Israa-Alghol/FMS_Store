using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMS_Store.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        public int Amount { get; set; }
        public double totalPrice { get; set; }

        [ForeignKey("productId")]
        public int productId { get; set; }

    }

    
}
