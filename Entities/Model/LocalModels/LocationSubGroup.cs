﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model.LocalModels
{
    [Table("_LocationSubGroup")]
    public class LocationSubGroup
    {
        [Key]
        public int ID { get; set; }
        public int GroupID { get; set; }
        [Required, MaxLength(50)]
        public string Value { get; set; }

        public LocationGroup LocationGroup { get; set; }
        public List<LocationNames> LocationNames { get; set; }
    }
}
