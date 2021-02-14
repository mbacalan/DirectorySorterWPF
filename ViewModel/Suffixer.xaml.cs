using IOHelper.Model;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace IOHelper
{
    /// <summary>
    /// Interaction logic for Prefixer.xaml
    /// </summary>
    public partial class Suffixer : Page
    {
        public Suffixer()
        {
            InitializeComponent();
        }

        private void PickDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                Multiselect = true,
            };

            if (dialog.ShowDialog() == true)
            {
                SuffixerModel.OriginalFiles.Clear();
                SuffixerModel.SuffixedFiles.Clear();
                SuffixerModel.ChosenPath = Path.GetDirectoryName(dialog.FileName);
                SuffixerModel.FoundFiles = dialog.FileNames;
                SuffixTextBox.IsEnabled = true;
                ChosenPathTextBlock.Text = SuffixerModel.ChosenPath;

                foreach (string file in SuffixerModel.FoundFiles)
                {
                    SuffixerModel.OriginalFiles.Add(file.Substring(file.LastIndexOf('\\') + 1));
                }
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < SuffixerModel.FoundFiles.Length; i++)
            {
                string originalName = SuffixerModel.FoundFiles[i];
                string newName = Path.Combine(SuffixerModel.ChosenPath, SuffixerModel.SuffixedFiles[i]);

                try
                {
                    File.Move(originalName, newName);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error suffixing file(s)! Make sure they are accessible and not in use.");
                }
            }

            MessageBox.Show("Suffixing Complete!");
        }

        private void SuffixTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SuffixTextBox.Text))
            {
                ConfirmButton.IsEnabled = false;
                SuffixerModel.SuffixedFiles.Clear();
                return;
            }

            SuffixerModel.Suffix = SuffixTextBox.Text;
            SuffixerModel.SuffixedFiles.Clear();
            ConfirmButton.IsEnabled = true;

            foreach (string file in SuffixerModel.FoundFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string fileExtension = Path.GetExtension(file);
                SuffixerModel.SuffixedFiles.Add($"{fileName}{SuffixerModel.Suffix}{fileExtension}");
            }
        }
    }
}