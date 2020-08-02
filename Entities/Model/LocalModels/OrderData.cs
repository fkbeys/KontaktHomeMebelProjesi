using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Entities.Model.LocalModels
{
    public class OrderData
    {
        public Orders order { get; set; }
        public OrderFileUpload orderFiles { get; set; }        
        public List<Visits> visitData { get; set; }
        public SelectList itemGroups { get; set; }
        public List<VisitImages> visitImages { get; set; }
        public List<Production> production { get; set; }
    }
}
