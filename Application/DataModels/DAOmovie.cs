using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataModels {
    public class DAOmovie {
        public int Id { get; set; }
        public string MovieTitle { get; set; }
        public string MoviePath { get; set; }
        public int MovieYear { get; set; }
    }
}
