using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DBModels
{
    public class WorkCenterOperation
    {
        public int ID { get; set; }
        public int WorkCenterID { get; set; }
        public int OperationID { get; set; }
        public double Speed { get; set; }
        public WorkCenter? WorkCenter { get; set; }
        public Operation Operation { get; set; }
    }
}
