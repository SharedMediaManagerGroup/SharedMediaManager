using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.FileHandler;
using MediaInfoLib;

namespace Application.Scraper {
    public class MediaInfoReader {
        private MediaInfo mInfo;
        private int currentStream = 0;   //default is 0
        private int streamAudioCount;
        private int streamVideoCount;
        private int streamSubCount;

        public MediaInfoReader() {
            mInfo = new MediaInfo();
        }

        public void CloseMediaInfo() {
            mInfo.Close();
            mInfo.Dispose();
        }

        public void OpenMediaFile(string filePath) {
            mInfo.Open(filePath);
            streamAudioCount = mInfo.Count_Get(StreamKind.Audio);
            streamVideoCount = mInfo.Count_Get(StreamKind.Video);
            streamSubCount = mInfo.Count_Get(StreamKind.Text);
        }

        public string GetCompleteInfo() {
            mInfo.Option("Complete");
            return mInfo.Inform();
        }

        // -------------------------------------------------------------------------
        // FILE STUFF
        // -------------------------------------------------------------------------

        public double GetFileSize() {
            string mediaFileSize = mInfo.Get(StreamKind.General, currentStream, "FileSize");
            double mediaSize = ((Double.Parse(mediaFileSize) / 1024) / 1024);  // convert to megabyte
            mediaSize = Math.Round(mediaSize, 2);
            return mediaSize;
        }

        public string GetFileFormat() {
            string mediaFileInfo= mInfo.Get(StreamKind.General, currentStream, "Format");
            return mediaFileInfo;
        }

        public string GetFileEncodedAppInfo() {
            string mediaFileInfo = mInfo.Get(StreamKind.General, currentStream, "Encoded_Application");
            return mediaFileInfo;
        }

        public string GetFileLibraryAppInfo() {
            string mediaFileInfo = mInfo.Get(StreamKind.General, currentStream, "Encoded_Library");
            return mediaFileInfo;
        }

        // -------------------------------------------------------------------------
        // VIDEO STUFF
        // -------------------------------------------------------------------------

        public int GetVideoStreamCount() {
            return streamVideoCount;
        }

        public long GetVideoBitrateKbps() {
            string mediaFileInfo = mInfo.Get(StreamKind.Video, currentStream, "BitRate");
            long vidKbps = TypeConvert.StringToLong(mediaFileInfo) / 100;  // convert to kbps
            return vidKbps;
        }

        public long GetVideoOverallBitrateKbps() {
            string mediaFileInfo = mInfo.Get(StreamKind.General, currentStream, "OverallBitRate");
            long vidOverallKbps = TypeConvert.StringToLong(mediaFileInfo) / 100;   // convert to kbps
            return vidOverallKbps;
        }

        public string GetDisplayAspectRatio() {
            string mediaFileInfo = mInfo.Get(StreamKind.Video, currentStream, "DisplayAspectRatio/String");
            return mediaFileInfo;
        }

        public int GetVideoResolutionWidth() {
            string mediaFileInfo = mInfo.Get(StreamKind.Video, currentStream, "Width");
            return TypeConvert.StringToInteger(mediaFileInfo);
        }

        public int GetVideoResolutionHeight() {
            string mediaFileInfo = mInfo.Get(StreamKind.Video, currentStream, "Height");
            return TypeConvert.StringToInteger(mediaFileInfo);
        }

        public long GetVideoDurationMinutes() {
            string mediaFileInfo = mInfo.Get(StreamKind.Video, currentStream, "Duration");
            long vidDurationMinutes = TypeConvert.StringToLong(mediaFileInfo);
            return vidDurationMinutes / 60000;
        }

        public string GetVideoDurationFormatted() {
            string mediaFileInfo = mInfo.Get(StreamKind.Video, currentStream, "Duration/String3");
            return mediaFileInfo;
        }

        public double GetVideoFramerate() {
            string mediaFileInfo = mInfo.Get(StreamKind.Video, currentStream, "FrameRate");
            return TypeConvert.StringToDouble(mediaFileInfo);
        }

        public string GetVideoSourceLanguage() {
            string mediaFileInfo = mInfo.Get(StreamKind.Video, currentStream, "Language/String");
            return mediaFileInfo;
        }

        public string GetVideoFormat() {
            string mediaFileInfo = mInfo.Get(StreamKind.Video, currentStream, "Format");
            return mediaFileInfo;
        }

        public string GetVideoBitrateMode() {
            string mediaFileInfo = mInfo.Get(StreamKind.Video, currentStream, "BitRate_Mode/String");
            return mediaFileInfo;
        }

        // -------------------------------------------------------------------------
        // AUDIO STUFF
        // -------------------------------------------------------------------------
        public int GetAudioStreamCount() {
            return streamAudioCount;
        }

        public List<string> GetAudioFormats() {
            List<string> list = new List<string>();
            for (int i = 0; i < streamAudioCount; i++) {
                string audioInf = mInfo.Get(StreamKind.Audio, i, "Format");
                list.Add(audioInf);
            }
            return list;
        }

        public List<string> GetAudioLanguages() {
            List<string> list = new List<string>();
            for (int i = 0; i < streamAudioCount; i++) {
                string audioInf = mInfo.Get(StreamKind.Audio, i, "Language/String");
                list.Add(audioInf);
            }
            return list;
        }

        public List<long> GetAudioBitratesKbps() {
            List<long> list = new List<long>();
            for (int i = 0; i < streamAudioCount; i++) {
                string audioInf = mInfo.Get(StreamKind.Audio, i, "BitRate");
                list.Add(TypeConvert.StringToLong(audioInf) / 1000);
            }
            return list;
        }

        public List<int> GetAudioChannels() {
            List<int> list = new List<int>();
            for (int i = 0; i < streamAudioCount; i++) {
                string audioInf = mInfo.Get(StreamKind.Audio, i, "Channel(s)");
                list.Add(TypeConvert.StringToInteger(audioInf));
            }
            return list;
        }

        // -------------------------------------------------------------------------
        // SUBS
        // -------------------------------------------------------------------------
        public int GetSubCount() {
            return streamSubCount;
        }

        public List<string> GetSubLanguages() {
            List<string> list = new List<string>();
            for (int i = 0; i < streamSubCount; i++) {
                string subInf = mInfo.Get(StreamKind.Text, i, "Language/String");
                list.Add(subInf);
            }
            return list;
        }

        public List<string> GetSubForced() {
            List<string> list = new List<string>();
            for (int i = 0; i < streamSubCount; i++) {
                string subInf = mInfo.Get(StreamKind.Text, i, "Forced");
                list.Add(subInf);
            }
            return list;
        }
    }
}
