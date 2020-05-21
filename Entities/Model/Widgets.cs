using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class Widgets
    {
        public int WaitingOrders { get; set; }
        public int ProcessingOrders { get; set; }
        public int SaleWaitingOrders { get; set; }
        public int ProductionOrders { get; set; }
    }
}
