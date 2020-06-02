using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductTest.Models
{
    public class Category
    {
        public string CategoryId { get; set; }

        public string CategoryName { get; set; }


        //Relation
        public ICollection<Product> Products { get; set; }
    }
}
