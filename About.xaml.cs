using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DENTAL_WPF
{
    public partial class About : Window
    {
        BitmapImage AboutBitmap;
        public About(bool th)
        {
            InitializeComponent();
            AboutBitmap = new BitmapImage();
            AboutBitmap.BeginInit();
            if (th) AboutBitmap.UriSource = new Uri("/DAbout.png", UriKind.Relative);
            else AboutBitmap.UriSource = new Uri("/LAbout.png", UriKind.Relative);
            AboutBitmap.EndInit();
            AboutImage.Source = AboutBitmap;
        }
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Close();
        }
        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
