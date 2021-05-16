using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DBModels
{
    public class Operation
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ProductTypeID { get; set; }
        public ProductType ProductType { get; set; }
        public virtual List<WorkCenterOperation> WorkCenterOperations  { get; set; }
    }
}
