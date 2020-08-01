using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Entities.Model.LocalModels
{
    public class SaveRecipe
    {
        public int order_No { get; set; }
        public int visit_No { get; set; }
        public List<Production> recipe_Items { get; set; }
        public int[] deleted_items { get; set; }
    }
}
