using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DBModels
{
    public class ProductType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual List<Product> Products { get; set; }
        public virtual List<Operation> Operations { get; set; }
    }
}
