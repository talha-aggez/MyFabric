using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DBModels
{
    public class Order
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DeadLine { get; set; }
        public Customer Customer { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }
    }
}
