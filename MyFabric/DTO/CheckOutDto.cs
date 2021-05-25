using Core.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFabric.DTO
{
    public class CheckOutDto
    {
        public int AppUserId { get; set; }
        public  List<OrderItem> OrderItems { get; set; }
        public DateTime? DeadLine { get; set; }
    }
}
