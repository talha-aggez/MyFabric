using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DBModels
{
    public class OrderItem
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Amount { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
