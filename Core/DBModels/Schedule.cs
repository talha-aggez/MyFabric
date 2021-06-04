using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DBModels
{
    public class Schedule
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int WorkCenterID { get; set; }
        public int OrderID { get; set; }

        public WorkCenter? WorkCenter { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
