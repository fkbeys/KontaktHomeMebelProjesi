using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model.MikroModels
{
    public class Products
    {
        [Key]
        public string product_code { get; set; }
        public string product_name { get; set; }
        public double? product_price { get; set; }
        public double? product_quantity { get; set; }
    }
}
