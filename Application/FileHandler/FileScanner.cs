using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.FileHandler {
    public class FileScanner {

        public List<String> ScanDirectories(string sourceDirectory) {
            //sourceDirectory = @"F:\[NETZWERK]\SoulseekShare";
            string[] extensions = {".mkv", ".avi", ".mp4"};

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var results = Directory.EnumerateFiles(sourceDirectory, "*", SearchOption.AllDirectories)
                .Where(s => extensions.Any(ext => ext.ToLower() == Path.GetExtension(s).ToLower()));

            stopWatch.Stop();
            Console.WriteLine("Time elapsed: " + stopWatch.Elapsed.Milliseconds);
            Console.WriteLine("Files indexed: " + results.Count());

            return results.ToList();
        }
    }
}
