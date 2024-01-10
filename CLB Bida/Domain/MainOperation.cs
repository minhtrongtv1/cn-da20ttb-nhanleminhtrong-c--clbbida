using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLB_Bida.Domain
{
    [Table("MainOperation")]
    public class MainOperation
    {
        [Key]
        public int Id { get; set; }
        public int TableId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool TableStatus { get; set; }
    }
}
