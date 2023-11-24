using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLB_Bida.Domain
{
    [Table("Table")]
    public class Table
    {
        [Key]
        public int TableId { get; set; }
        public string TableName { get; set; }
        public decimal UnitPrice { get; set; }
        public bool TableStatus { get; set; }
        

    }
}
