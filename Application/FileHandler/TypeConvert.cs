using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FileHandler {
    public class TypeConvert {

        public static long StringToLong(string text) {
            long result = 0;
            if (Int64.TryParse(text, out result)) {
                return result;
            }
            return result;
        }

        public static int StringToInteger(string text) {
            int result = 0;
            if (Int32.TryParse(text, out result)) {
                return result;
            }
            return result;
        }

        public static double StringToDouble(string text) {
            double result = 0.0;
            if (Double.TryParse(text, out result)) {
                return result;
            }
            return result;
        }

    }
}
