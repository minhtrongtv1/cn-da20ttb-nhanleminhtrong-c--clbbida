using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLB_Bida.Domain
{
    [Table("OutsideOrder")]
    public class OutsideOrder
    {
        [Key]
        public int InternalId { get; set; }
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int OrderQty { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
