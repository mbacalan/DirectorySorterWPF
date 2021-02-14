using System.Collections.ObjectModel;

namespace IOHelper.Model
{
    internal class PrefixerModel
    {
        private static string _path;
        private static string _prefix;
        private static string[] _foundFiles;

        public static ObservableCollection<string> OriginalFiles { get; set; } = new ObservableCollection<string>();
        public static ObservableCollection<string> PrefixedFiles { get; set; } = new ObservableCollection<string>();

        public static string Prefix
        {
            get { return _prefix; }
            set { _prefix = value; }
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