using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Application.DataAccess;
using Application.DataModels;
using Application.FileHandler;
using Application.Logging;
using Application.Scraper;
using Application.XRel;
using Application.XRel.XRelData;
using TMDbLib.Objects.Search;
using Path = System.IO.Path;

namespace SharedMediaManager {

    public partial class MainWindow : Window {

        public MediaInfoReader mir;
        public TMDBinfo tmdb = new TMDBinfo();
        public FileScanner scan = new FileScanner();

        public MainWindow() {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Test();
        }

        public void Test() {
            // file directory scanner example
            List<string> filesList = scan.ScanDirectories(@"D:\DOWNLOADS\_filmefertig");

            dataGrid.Items.Clear();
            dataGrid.ItemsSource = CreateFileDataList(filesList);
        }

        public List<FileData> CreateFileDataList(List<string> fileList) {
            List<FileData> fileDataList = new List<FileData>();
            FileData fData;
            foreach (var file in fileList) {
                fData = new FileData();

                fData.filePath = file;
                fData.fileName = StringStuff.GetMovieNameFromFilename(Path.GetFileNameWithoutExtension(file));
                fData.fileType = Path.GetExtension(file);
                fData.movieYear = StringStuff.GetYearFromMovieFilename(Path.GetFileNameWithoutExtension(file));

                fileDataList.Add(fData);
            }

            return fileDataList;
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            FileData selectedItem = (FileData) dataGrid.SelectedItem;
            ShowMediaData(selectedItem);
        }

        private void ShowMediaData(FileData fileItem) {
            // MediaInfo & TMDB & Metacritic examples
            StringBuilder result = new StringBuilder();
            mir = new MediaInfoReader();
            mir.OpenMediaFile(fileItem.filePath);

            SearchMovie mov = tmdb.GetMovieInfoByTitle(fileItem.fileName, fileItem.movieYear);

          
            result.AppendLine("Filename: " + fileItem.fileName);
            result.AppendLine("MovieYear: " + fileItem.movieYear);
            result.AppendLine();
            result.AppendLine(MetacriticInfo.GetMetacriticInfo(fileItem.fileName));
            //result.AppendLine("TMDBTitleByID: " + tmdb.GetMovieInfoByID(47964).Title);
            result.AppendLine("TMDBSearch Title: " + mov.Title);
            result.AppendLine("TMDBSearch OriginalTitle: " + mov.OriginalTitle);
            result.AppendLine("TMDBSearch ID: " + mov.Id);
            result.AppendLine("TMDBSearch ReleaseDate: " + mov.ReleaseDate);
            result.AppendLine("TMDBSearch OriginalLanguage: " + mov.OriginalLanguage);
            result.AppendLine("TMDBSearch Popularity: " + mov.Popularity);
            result.AppendLine("TMDBSearch VoteAverage: " + mov.VoteAverage);
            result.AppendLine("TMDBSearch PosterPath: " + mov.PosterPath);
            result.AppendLine("TMDBSearch BackdropPath: " + mov.BackdropPath);
            result.Append("TMDBSearch GenreIDs: ");
            foreach (var genreID in mov.GenreIds) {
                result.Append(genreID + ";");
            }
            result.AppendLine();
            result.AppendLine("TMDBSearch Overview: \n" + mov.Overview);
            result.AppendLine();
            result.AppendLine("***********************************************************************");
            result.AppendLine(mir.GetCompleteInfo());
            result.AppendLine("FileSize: " + mir.GetFileSize());
            result.AppendLine("FileFormat: " + mir.GetFileFormat());
            result.AppendLine("FileEncodedAppInfo: " + mir.GetFileEncodedAppInfo());
            result.AppendLine("FileLibraryAppInfo: " + mir.GetFileLibraryAppInfo());
            result.AppendLine();
            result.AppendLine("VideoStreamCount: " + mir.GetVideoStreamCount());
            result.AppendLine("VideoBitrateKbps: " + mir.GetVideoBitrateKbps());
            result.AppendLine("VideoOverallBitrateKbps: " + mir.GetVideoOverallBitrateKbps());
            result.AppendLine("VideoResolution: " + mir.GetVideoResolutionWidth() + "x" + mir.GetVideoResolutionHeight());
            result.AppendLine("VideoDisplayAspectRatio: " + mir.GetDisplayAspectRatio());
            result.AppendLine("VideoDurationMinutes: " + mir.GetVideoDurationMinutes());
            result.AppendLine("VideoDurationFormatted: " + mir.GetVideoDurationFormatted());
            result.AppendLine("VideoFramerate: " + mir.GetVideoFramerate());
            result.AppendLine("VideoSourceLanguage: " + mir.GetVideoSourceLanguage());
            result.AppendLine("VideoFormat: " + mir.GetVideoFormat());
            result.AppendLine("VideoBitrateMode: " + mir.GetVideoBitrateMode());
            result.AppendLine();
            result.AppendLine("AudioStreamCount: " + mir.GetAudioStreamCount());
            result.AppendLine("AudioLines: ");
            for (int i = 0; i < mir.GetAudioStreamCount(); i++) {
                result.AppendLine("Audio #" + i + ": " + mir.GetAudioLanguages()[i] + "/Format: " + mir.GetAudioFormats()[i] 
                                + "/Channels: " + mir.GetAudioChannels()[0] + "/Kbps: " + mir.GetAudioBitratesKbps()[0]);
            }
            result.AppendLine();
            result.AppendLine("SubStreamCount: " + mir.GetSubCount());
            result.AppendLine("SubLines: ");
            for (int i = 0; i < mir.GetSubCount(); i++) {
                result.AppendLine("Sub #" + i + ": " + mir.GetSubLanguages()[i] + "/ Forced: " + mir.GetSubForced()[i]);
            }

            mir.CloseMediaInfo();
            textBox.Text = result.ToString();
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            // XREl nfo fetch example
            XRelInfo xinf = new XRelInfo();
            StringBuilder sb = new StringBuilder();
            XRelLatest xrel = xinf.GetLatestHDMovies();
            sb.AppendLine("Total Count:" + xrel.total_count);
            sb.AppendLine("Current Page: " + xrel.pagination.current_page);
            sb.AppendLine("ListCount: " + xrel.list.Count);
            sb.AppendLine();
            foreach (var xItem in xrel.list) {
                if (xinf.GetLatestHDMoviesStringFilter(xItem.dirname, xItem.audio_type)) {
                    sb.AppendLine(xItem.dirname);
                }
            }

            textBox.Text = sb.ToString();
        }

        private void button1_Click(object sender, RoutedEventArgs e) {
            // SQLite & Dapper Example

            IMovieRepository rep = new SqLiteMovieRepository();
            var movie = new DAOmovie() {
                MovieTitle = "The Rock",
                MoviePath = "test",
                MovieYear = 1996
            };
            rep.SaveMovie(movie);
           
            DAOmovie retrievedMovie = rep.GetMovie(movie.Id);

            // Check query result
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SQLite & Dapper query result:");
            sb.AppendLine("ID: " + retrievedMovie.Id);
            sb.AppendLine("MovieTitle: " + retrievedMovie.MovieTitle);
            sb.AppendLine("MoviePath: " + retrievedMovie.MoviePath);
            sb.AppendLine("MovieYear: " + retrievedMovie.MovieYear);
            textBox.Text = sb.ToString();
        }

        private void button2_Click(object sender, RoutedEventArgs e) {
            // example for logging
            LogHelper.AddInfoLog("Guten Tag", GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
            textBox.Text = LogHelper.GetCurrentLogfileContent();
        }
    }
}
