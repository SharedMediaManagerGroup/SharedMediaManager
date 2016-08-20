using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMetacritic;

namespace Application.Scraper {
    public class MetacriticInfo {

        public static string GetMetacriticInfo(string movieName) {
            var metacriticResults = Metacritic.SearchFor().Movies().UsingText(movieName);
            var metaList = metacriticResults.ToList();

            return "Metacritic Results Count: " + metaList.Count + "/" + metaList[0].Name + "(Score: " + metaList[0].CriticScore + ")";
        }

    }
}
