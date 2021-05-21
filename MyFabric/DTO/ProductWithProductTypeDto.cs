using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFabric.DTO
{
    public class ProductWithProductTypeDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductTypeID { get; set; }
        public string ProductTypeName { get; set; }
        public bool IsSalable { get; set; }
   
    }
}
