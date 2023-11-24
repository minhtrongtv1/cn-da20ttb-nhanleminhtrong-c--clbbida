using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLB_Bida.Domain
{
    [Table("ORDER_HEADER")]
    public class OrderHeader
    {
        [Key]
        public int InternalOrderNum { get; set; }
        public int TableId { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public bool TableStatus { get; set; }
    }
}
