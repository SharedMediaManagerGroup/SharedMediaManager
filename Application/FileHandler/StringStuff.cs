using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Application.FileHandler {
    public class StringStuff {

        public static int GetYearFromMovieFilename(string movieFilename) {
            string year = Regex.Match(movieFilename, @"\(([^)]*)\)").Groups[1].Value;

            //  check if its valid year (4 digits)
            if (CheckStringIsNumeric(year, NumberStyles.Integer)) {
                int extractedYear = Int32.Parse(year);
                if ((extractedYear > 1800) && (extractedYear < 2100)) {
                    return extractedYear;
                }
            }
            return 0;
        }

        public List<string> GetBracketInfosFromFilename(string movieFilename) {
            List<string> matchList = new List<string>();
            Regex regex = new Regex("\\\\((.*?)\\\\)");
            Match regexMatcher = regex.Match(movieFilename);

            while (regexMatcher.Success) {
                matchList.Add(regexMatcher.Value);
                regexMatcher = regexMatcher.NextMatch();
            }

            return matchList;
        }

        public static string GetMovieNameFromFilename(string fileName) {
            int posBracket = fileName.IndexOf('(');

            if (posBracket != -1) {
                return fileName.Substring(0, posBracket).TrimEnd();
            }
            return fileName;
        }

        public int GetTmdbFromNfoFile(string nfopath) {
            string ct = Convert.ToBase64String(File.ReadAllBytes(nfopath));
            int result = -1;
        
            if (ct.Contains("www.themoviedb.org")){
                int startpos = ct.IndexOf("www.themoviedb.org", StringComparison.Ordinal);
            int endpos = ct.IndexOf("-", startpos, StringComparison.Ordinal);

            string sub1 = ct.Substring(startpos, endpos);

                try{
                    result = Int32.Parse(sub1.Substring(sub1.LastIndexOf("/", StringComparison.Ordinal) + 1));
                } catch (Exception ex){
                    result = -1;
                    Console.WriteLine("Error: cannot parse tmdb id from nfo file " + nfopath);
               }
            }
            return result;
        }

        public static bool CheckStringIsNumeric(string val, NumberStyles NumberStyle) {
            double result;
            return Double.TryParse(val, NumberStyle, CultureInfo.CurrentCulture, out result);
        }

    }
}