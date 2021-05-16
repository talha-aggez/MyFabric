using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DBModels
{
    public class WorkCenter
    {
        public int ID { get; set; }
        public string WorkCenterName { get; set; }
        public bool Active { get; set; }
        public virtual List<WorkCenterOperation> WorkCenterOperations { get; set; }

    }
}
