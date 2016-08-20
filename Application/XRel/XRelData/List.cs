using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.XRel.XRelData {
    public class List {
        public string id { get; set; }
        public string dirname { get; set; }
        public string link_href { get; set; }
        public int time { get; set; }
        public string group_name { get; set; }
        public Size size { get; set; }
        public string video_type { get; set; }
        public string audio_type { get; set; }
        public int num_ratings { get; set; }
        public ExtInfo ext_info { get; set; }
        public int comments { get; set; }
        public string proof_url { get; set; }
        public Flags flags { get; set; }
    }
}
