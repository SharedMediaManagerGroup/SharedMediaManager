using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DataModels;
using Dapper;

namespace Application.DataAccess {
    public class SqLiteMovieRepository : SqLiteBaseRepository, IMovieRepository {
        public DAOmovie GetMovie(long id) {
            if (!File.Exists(DbFile)) return null;

            using (var cnn = SimpleDbConnection()) {
                cnn.Open();
                DAOmovie result = cnn.Query<DAOmovie>(
                    @"SELECT Id, MovieTitle, MoviePath, MovieYear
                    FROM DAOmovie
                    WHERE Id = @id", new { id }).FirstOrDefault();
                return result;
            }
        }



        public void SaveMovie(DAOmovie movie) {
            if (!File.Exists(DbFile)) {
                CreateDatabase();
            }

            using (var cnn = SimpleDbConnection()) {
                cnn.Open();
                movie.Id = cnn.Query<int>(
                    @"INSERT INTO DAOmovie 
                    ( MovieTitle, MoviePath, MovieYear ) VALUES 
                    ( @MovieTitle, @MoviePath, @MovieYear );
                    select last_insert_rowid()", movie).First();
            }
        }

        private static void CreateDatabase() {
            using (var cnn = SimpleDbConnection()) {
                cnn.Open();
                cnn.Execute(
                    @"create table DAOmovie
                      (
                         ID                                  integer primary key AUTOINCREMENT,
                         MovieTitle                          varchar(100) not null,
                         MoviePath                           varchar(100) not null,
                         MovieYear                           integer not null
                      )");
            }
        }
    }
}
