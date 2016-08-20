using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Application.XRel.XRelData;
using Newtonsoft.Json;

namespace Application.XRel {
    public class XRelInfo {



        public XRelLatest GetLatestHDMovies() {
            string category = "HDTV";
            string type = "movie";
            int pages = 1; // change to 5

            string reqURL = @"https://api.xrel.to/v2/release/browse_category.json?category_name=" + category + "&ext_info_type=" + type + "&per_page=100&page=" + pages;
            string reqResult = GetXRelJson(reqURL);
            XRelLatest resultsList = JsonConvert.DeserializeObject<XRelLatest>(reqResult);

            return resultsList;
        }

        public bool GetLatestHDMoviesStringFilter(string dirname, string audiotype) {
            if (dirname.Contains("1080p") &&
                dirname.Contains("BluRay") &&
                //dirname.Contains(currentYear)) &&
                !audiotype.Contains("LineDubbed")) {
                return true;
            }
            return false;
        }

        private string GetXRelJson(string url) {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream()) {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            } catch (WebException ex) {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream()) {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }
    }
}
