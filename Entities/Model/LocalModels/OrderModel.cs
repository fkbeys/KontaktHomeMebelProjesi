using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model.LocalModels
{
    public class OrderModel
    {
        public Orders Orders { get; set; }
        public List<LocationGroup> LocationGroup { get; set; }
        public List<LocationSubGroup> LocationSubGroup { get; set; }
        public List<LocationNames> LocationNames { get; set; }
    }
}
