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
    public partial class Prefixer : Page
    {
        public Prefixer()
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
                PrefixerModel.OriginalFiles.Clear();
                PrefixerModel.PrefixedFiles.Clear();
                PrefixerModel.ChosenPath = Path.GetDirectoryName(dialog.FileName);
                PrefixerModel.FoundFiles = dialog.FileNames;
                PrefixTextBox.IsEnabled = true;
                ChosenPathTextBlock.Text = PrefixerModel.ChosenPath;

                foreach (string file in PrefixerModel.FoundFiles)
                {
                    PrefixerModel.OriginalFiles.Add(file.Substring(file.LastIndexOf('\\') + 1));
                }
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < PrefixerModel.FoundFiles.Length; i++)
            {
                string originalName = PrefixerModel.FoundFiles[i];
                string newName = Path.Combine(PrefixerModel.ChosenPath, PrefixerModel.PrefixedFiles[i]);

                try
                {
                    File.Move(originalName, newName);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error prefixing file(s)! Make sure they are accessible and not in use.");
                }
            }

            MessageBox.Show("Prefixing Complete!");
        }

        private void PrefixTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PrefixTextBox.Text))
            {
                ConfirmButton.IsEnabled = false;
                PrefixerModel.PrefixedFiles.Clear();
                return;
            }

            PrefixerModel.Prefix = PrefixTextBox.Text;
            PrefixerModel.PrefixedFiles.Clear();
            ConfirmButton.IsEnabled = true;

            foreach (string file in PrefixerModel.FoundFiles)
            {
                PrefixerModel.PrefixedFiles.Add(PrefixerModel.Prefix + file.Substring(file.LastIndexOf('\\') + 1));
            }
        }
    }
}