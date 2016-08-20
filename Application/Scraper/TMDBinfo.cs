using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.FileHandler;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace Application.Scraper {
    public class TMDBinfo {

        private TMDbClient client;
        private string LANGUAGE;
        private string TMDB_API_KEY;

        public TMDBinfo() {
            LANGUAGE = "de";
            TMDB_API_KEY = "dec0b87fe8f35746c1e96d2fa8ba4873";
            client = new TMDbClient(TMDB_API_KEY);
            client.DefaultLanguage = LANGUAGE;
        }

        public Movie GetMovieInfoByID(int movieId) {
            Movie movie = client.GetMovieAsync(movieId, LANGUAGE).Result;
            return movie;
        }


        public SearchMovie GetMovieInfoByTitle(string movieName, int year = 0) {
            SearchContainer<SearchMovie> results = new SearchContainer<SearchMovie>();

            if (year != 0) {
                results = client.SearchMovieAsync(movieName, LANGUAGE, 0, false, year).Result;
            } else {
                results = client.SearchMovieAsync(movieName, LANGUAGE).Result;
            }
            if (results.TotalResults == 0) {
                return null;
            }
            return results.Results[0];
        }

        public SearchMovie GetMovieInfoByFilename(string movieFileName) {
            string movieName = StringStuff.GetMovieNameFromFilename(movieFileName);
            int year = StringStuff.GetYearFromMovieFilename(movieFileName);
            return GetMovieInfoByTitle(movieName, year);
        }

    }
}
