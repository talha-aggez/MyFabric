using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFabric.DTO
{
    public class OrderListDto
    {
        public DateTime? OrderDate { get; set; }
        public DateTime? DeadLine { get; set; }
        public string ProductName { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Amount { get; set; }
    }
}
