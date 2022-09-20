using System.Collections.Generic;

namespace FMS_Store.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int totalPrice{ get; set; }

        public List<Product> products { get; set; }
    }
}
