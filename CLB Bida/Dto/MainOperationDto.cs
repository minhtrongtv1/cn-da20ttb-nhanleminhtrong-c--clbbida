using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLB_Bida.Dto
{
    public class MainOperationDto
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string TableStatus { get; set; }
    }
}
