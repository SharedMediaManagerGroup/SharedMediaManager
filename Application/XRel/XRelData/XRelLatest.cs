using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.XRel.XRelData {
    public class XRelLatest {
        public int total_count { get; set; }
        public Pagination pagination { get; set; }
        public List<List> list { get; set; }
    }
}
