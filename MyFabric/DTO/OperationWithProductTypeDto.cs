using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFabric.DTO
{
    public class OperationWithProductTypeDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ProductTypeID { get; set; }
        public string ProductTypeName { get; set; }
    }
}
