using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OrderSearch
    {
        public string firstDate { get; set; }
        public string lastDate { get; set; }
        public bool deletedOrders { get; set; }
        public bool  activeOrders { get; set; }
        public string sellerCode { get; set; }
        public string storeCode { get; set; }
    }
}
