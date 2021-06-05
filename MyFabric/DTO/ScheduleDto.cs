using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFabric.DTO
{
    public class ScheduleDto
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int WorkCenterID { get; set; }
        public int OrderID { get; set; }
        public double Speed { get; set; }
    }
}
