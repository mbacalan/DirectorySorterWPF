using System.Collections.ObjectModel;

namespace IOHelper.Model
{
    internal class SuffixerModel
    {
        private static string _path;
        private static string _suffix;
        private static string[] _foundFiles;

        public static ObservableCollection<string> OriginalFiles { get; set; } = new ObservableCollection<string>();
        public static ObservableCollection<string> SuffixedFiles { get; set; } = new ObservableCollection<string>();

        public static string Suffix
        {
            get { return _suffix; }
            set { _suffix = value; }
        }

        public static string ChosenPath
        {
            get { return _path; }
            set { _path = value; }
        }

        public static string[] FoundFiles
        {
            get { return _foundFiles; }
            set { _foundFiles = value; }
        }
    }
}