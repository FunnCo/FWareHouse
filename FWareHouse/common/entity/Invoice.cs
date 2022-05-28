using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace FWareHouse.common.entity
{
    class Invoice
    {
        [Key]
        public int Id { get; set; }
        public int product_id { get; set; }
        public int count { get; set; }
        public int responsible_employee { get; set; }
        public string invoice_code {get; set; }

    }
}
