using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLB_Bida.Domain
{
    [Table("ORDER_DETAIL")]
    public class OrderDetail
    {
        [Key]
        public int InternalOrderLineNum { get; set; }
        public int InternalOrderNum { get; set; }        
        public int ProductCode { get; set; }
        public string ProductName { get; set; }
        public int OrderQty { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
