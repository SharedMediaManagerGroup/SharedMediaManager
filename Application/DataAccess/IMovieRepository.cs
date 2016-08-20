using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DataModels;

namespace Application.DataAccess {

    public interface IMovieRepository {
        DAOmovie GetMovie(long id);
        void SaveMovie(DAOmovie customer);
    }

}
