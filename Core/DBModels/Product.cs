using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.DBModels
{
    public class Product
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public int ProductTypeID { get; set; }
        public bool IsSalable { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }
        public ProductType ProductType { get; set; }
        public virtual List<SubProductTree> SubProductTrees{ get; set; }
        //public virtual List<Product> SubProducts { get; set; }
    }
}
