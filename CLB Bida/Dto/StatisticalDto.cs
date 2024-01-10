using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLB_Bida.Dto
{
    public class StatisticalDto
    {
        public int? Index { get; set; }
        public int? ProductCode { get; set; }
        public string ProductName { get; set; }
        public int? CatId { get; set; }
        public string CatName { get; set; }
        public int? TotalQty { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
