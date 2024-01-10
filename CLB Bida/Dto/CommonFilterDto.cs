using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLB_Bida.Dto
{
    public class CommonFilterDto
    {
        public int InternalOrderNum { get; set; }
        public int InternalOrderLineNum { get; set; }
        public int ProductCode { get; set; } = -1;
        public int CatId { get; set; } = -1;
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
    }
}
