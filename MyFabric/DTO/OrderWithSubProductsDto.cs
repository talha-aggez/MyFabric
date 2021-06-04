using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFabric.DTO
{
    public class OrderWithSubProductsDto
    {
        public int ProductId { get; set; }
        public List<SubProductDto> SubProducts { get; set; }
      
    }
}
