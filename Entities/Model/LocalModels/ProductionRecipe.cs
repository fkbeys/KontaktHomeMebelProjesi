using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Entities.Model.LocalModels
{
   public class ProductionRecipe
    {
        public Orders order { get; set; }
        public Visits visits { get; set; }
        public SelectList itemGroups { get; set; }
    }
}
