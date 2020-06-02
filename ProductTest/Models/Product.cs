using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductTest.Models
{
    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }



        public Category Category { get; set; }
    }
}
