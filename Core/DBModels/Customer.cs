using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DBModels
{
    public class Customer
    {
        public int   ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
