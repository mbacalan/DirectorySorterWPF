using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;

namespace IOHelper.Model
{
    internal class ResorterModel
    {
        private static string _path;
        private static int _index = 1;
        private static DirectoryInfo[] _folders;

        public static Dictionary<string, string> FoldersDict = new Dictionary<string, string>();
        public static ObservableCollection<DirectoryInfo> OriginalFolders { get; set; } = new ObservableCollection<DirectoryInfo>();
        public static ObservableCollection<ListBoxItem> SortedFolders { get; set; } = new ObservableCollection<ListBoxItem>();

        public static string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        public static int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        public static DirectoryInfo[] Folders
        {
            get { return _folders; }
            set { _folders = value; }
        }
    }
}