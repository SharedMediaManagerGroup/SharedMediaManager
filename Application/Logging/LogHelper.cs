using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Application.Logging {
    public class LogHelper {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static bool LOGGING_ENABLED;

        // setup logger from external sources
        public static bool LoggingEnabled {
            get { return LOGGING_ENABLED; }
            set {
                LOGGING_ENABLED = value;
                if (LOGGING_ENABLED) {
                    LogManager.EnableLogging();
                } else {
                    LogManager.DisableLogging();
                }
            }
        }

        public static string GetCurrentLogfileContent() {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + @"logs\CurrentLogSetbuilderData.log";
            if (!File.Exists(fileName)) {
                return "";
            }
            string readText = File.ReadAllText(fileName);
            return readText;
        }

        // functions for external log entries
        public static void AddInfoLog(string entry, string classmethodname = "") { logger.Info(classmethodname + "|" + entry); }
        public static void AddWarnLog(string entry, string classmethodname = "") { logger.Warn(classmethodname + "|" + entry); }
        public static void AddErrorLog(string entry, string classmethodname = "") { logger.Error(classmethodname + "|" + entry); }
        public static void AddFatalLog(string entry, string classmethodname = "") { logger.Fatal(classmethodname + "|" + entry); }
    }
}
