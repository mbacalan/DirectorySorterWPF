using IOHelper.Model;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace IOHelper
{
    /// <summary>
    /// Interaction logic for Resorter.xaml
    /// </summary>
    public partial class Resorter : Page
    {
        public Resorter()
        {
            InitializeComponent();
        }

        private void DirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                IsFolderPicker = true
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                ResorterModel.SortedFolders.Clear();
                ResorterModel.SortedFolders.Clear();
                ResorterModel.FoldersDict.Clear();
                ResorterModel.Index = 1;
                ResorterModel.Path = Path.GetFullPath(dialog.FileName);
                ResorterModel.Folders = new DirectoryInfo(ResorterModel.Path).GetDirectories("*");
                ConfirmButton.IsEnabled = true;

                foreach (DirectoryInfo directory in ResorterModel.Folders)
                {
                    ResorterModel.OriginalFolders.Add(directory);
                }

                OriginalFolders.ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;
            }
        }

        private void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
        {
            // If containers have been generated
            if (OriginalFolders.ItemContainerGenerator.Status == System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
            {
                // Remove event
                OriginalFolders.ItemContainerGenerator.StatusChanged -= ItemContainerGenerator_StatusChanged;

                foreach (var item in OriginalFolders.Items)
                {
                    var listItem = (ListBoxItem)OriginalFolders.ItemContainerGenerator.ContainerFromItem(item);
                    listItem.MouseDoubleClick += new MouseButtonEventHandler(OnDoubleClick);
                }
            }
        }

        private void OnDoubleClick(object sender, RoutedEventArgs e)
        {
            var listItem = (ListBoxItem)OriginalFolders.ItemContainerGenerator.ContainerFromIndex(OriginalFolders.SelectedIndex);
            var newListItem = new ListBoxItem
            {
                Content = $"{ResorterModel.Index} - {listItem.Content}"
            };

            listItem.IsEnabled = false;
            ResorterModel.SortedFolders.Add(newListItem);
            ResorterModel.FoldersDict.Add(
                $"{listItem.Content}",
                $@"{ResorterModel.Path}\{ResorterModel.Index} - {ResorterModel.Folders[OriginalFolders.SelectedIndex].Name}"
            );
            ResorterModel.Index++;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var entry in ResorterModel.FoldersDict)
            {
                try
                {
                    Directory.Move(entry.Key, entry.Value);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error re-sorting folder(s)! Make sure they are accessible and not in use.");
                }
            }

            MessageBox.Show("Re-Sorting Complete!");
        }
    }
}