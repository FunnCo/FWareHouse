using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWareHouse.common.entity
{
    internal class StoredProduct
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int stored { get; set; }
        public string measure_unit { get; set; }
        public double volume { get; set; }
        public double weight { get; set; }
        public string manufacturer { get; set; }
        public string storage_requirements { get; set; }


    }
}
