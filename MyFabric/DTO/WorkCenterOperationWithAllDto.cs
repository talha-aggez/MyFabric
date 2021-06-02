using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFabric.DTO
{
    public class WorkCenterOperationWithAllDto
    {
        public int ID { get; set; }
        public int WorkCenterID { get; set; }
        public int OperationID { get; set; }
        public double Speed { get; set; }
        public string WorkCenterName { get; set; }
        public string OperationName { get; set; }
    }
}
