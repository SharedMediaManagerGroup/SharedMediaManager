using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logging {
    public class StopwatchHelper {
        // Stopwatch helper class to measure performance and return elapsed time in milliseconds
        private static Stopwatch watch = new Stopwatch();

        public static void Start() {
            if (watch.IsRunning) {
                watch.Stop();
            }
            watch.Reset();
            watch.Start();
        }

        public static long Stop() {
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        private static double ConvertMillisecondsToSeconds(double milliseconds) {
            return TimeSpan.FromMilliseconds(milliseconds).TotalSeconds;
        }

    }
}
