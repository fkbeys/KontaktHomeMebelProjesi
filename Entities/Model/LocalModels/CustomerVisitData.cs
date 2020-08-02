using Entities.Model.LocalModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Entities
{
    public class CustomerVisitData
    {
        public Orders order { get; set; }
        public OrderFileUpload orderFiles { get; set; }
        [Required]
        public List<Visits> visitData { get; set; }
        public SelectList itemGroups { get; set; }
        public List<VisitImages> visitImages { get; set; }       
    }
}
