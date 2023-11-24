using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLB_Bida.Dto
{
    public class TableDto
    {
        public int TableId { get; set; }
        public string TableName { get; set; }
        public decimal UnitPrice { get; set; }
        public string TableStatus { get; set; }

    }
}
