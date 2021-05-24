using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFabric.DTO
{
    public class SubProductWithAllDto
    {
        public int SubProductId { get; set; }
        public string SubProductName { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int Amount { get; set; }
    }
}
