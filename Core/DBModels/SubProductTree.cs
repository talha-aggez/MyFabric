using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DBModels
{
    public class SubProductTree
    {
        public int ID { get; set; }
        public int SubProductID { get; set; }
        public int ProductID { get; set; }
        public int  Amount { get; set; }
        //public Product SubProducts{ get; set; }
        public Product Product { get; set; }
    }
}
