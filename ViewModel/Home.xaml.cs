using System.Windows;
using System.Windows.Controls;

namespace IOHelper
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }

        private void DirectoryResorterButton_Click(object sender, RoutedEventArgs e)
        {
            Resorter restorter = new Resorter();
            this.NavigationService.Navigate(restorter);
        }

        private void PrefixerButton_Click(object sender, RoutedEventArgs e)
        {
            Prefixer prefixer = new Prefixer();
            this.NavigationService.Navigate(prefixer);
        }

        private void SuffixerButton_Click(object sender, RoutedEventArgs e)
        {
            Suffixer suffixer = new Suffixer();
            this.NavigationService.Navigate(suffixer);
        }
    }
}