using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWareHouse.common.entity
{
    [Keyless]
    class InvoiceInfo
    {
        public string name { get; set; }
        public int count { get; set; }
        public string employee { get; set; }
        public string measure_unit { get; set; }
        public string sender_name { get; set; }
        public string receiver_name { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime operation_date { get; set; }

    }
}
