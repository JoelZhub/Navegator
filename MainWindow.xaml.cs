using System.Runtime.Intrinsics.Arm;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Navegator
{
    public partial class MainWindow : Window
    {
        private string searchUrl = "";

        private List<string> urls = new List<string>();   

        public MainWindow()
        {
            InitializeComponent();
             
        }


        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image image)
                changeView(image.Tag.ToString()!);
        }

        private void changeView(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                bool valid = Regex.IsMatch(url, @"^[a-zA-Z0-9]+\.com$");
                if (valid)
                {
          
                    webView.Source = new Uri(url);
                }
                string urlBase = $"https://www.google.com/search?q={Uri.EscapeDataString(url!)}";
                webView.Source = new Uri(urlBase);
                webView.Visibility = Visibility.Visible;
                main.Visibility = Visibility.Hidden;
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox search)
                search.Text = "";
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox search)
            {
                if (search.Text == "")
                    search.Text = "Buscar con google o introduzca la url";
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(sender is TextBox search)
            {
                if (e.Key == Key.Enter)
                    if(search.Text != "Buscar con google o introduzca la url")
                        changeView(search.Text);
            }
        }

        
    }
}