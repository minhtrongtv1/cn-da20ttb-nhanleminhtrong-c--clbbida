using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLB_Bida.Dto
{
    public class OrderHeaderDto
    {
        public int InternalOrderNum { get; set; }
        public int TableId { get; set; }
        public string TableName { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public bool TableStatus { get; set; }
    }
}
