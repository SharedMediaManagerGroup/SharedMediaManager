using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.XRel.XRelData {
    public class Pagination {
        public int current_page { get; set; }
        public int per_page { get; set; }
        public int total_pages { get; set; }
    }
}
