using System.Runtime.Intrinsics.Arm;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Navegator
{
    public partial class MainWindow : Window
    {
        private string searchUrl = "";

        private Stack<string> backUrl = new Stack<string>();

        private Stack<string> fowardUrl = new Stack<string>();

        private bool isNavigate = false;

        private int index = -1;

        private bool help = false;

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
                    backUrl.Push(url);
                    searchUrl = url;
                    webView.Source = new Uri(url);
                    index++;
                }
                string urlBase = $"https://www.google.com/search?q={Uri.EscapeDataString(url!)}";
                searchUrl = urlBase;
                backUrl.Push(searchUrl);
                fowardUrl.Clear();
                webView.Source = new Uri(searchUrl);
                index++;
                webView.Visibility = Visibility.Visible;
                main.Visibility = Visibility.Hidden;
                isNavigate = true;
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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            if(sender is  Button button)
            {
              
                if(button.Tag.ToString() == "btnRefresh" && !isNavigate)
                {
                    main.Visibility = Visibility.Visible;
                    webView.Visibility  =Visibility.Hidden;
                }
                else if(button.Tag.ToString() == "btnRefresh" && isNavigate)
                {
                    webView.Source = new Uri(searchUrl);
                }
                if(backUrl.Count() > 0 && button.Tag.ToString() == "btnBefore")
                {
                    fowardUrl.Push(searchUrl);
                    searchUrl = backUrl.Pop();
                    index--;
                    if (index == -1)
                    {
                        main.Visibility = Visibility.Visible;
                        webView.Visibility = Visibility.Hidden;
                    }
                }
                else if(fowardUrl.Count() > 0 && button.Tag.ToString() == "btnAfter")
                {
                    backUrl.Push(searchUrl);
                    searchUrl = fowardUrl.Pop();
                    index++;
                }


            }
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button mouseEnter)
            {
                if (!isNavigate)
                    mouseEnter.Cursor = Cursors.Hand;
            }
        }

        private void Button_Click_Help(object sender, RoutedEventArgs e)
        {
            if(sender is Button btn)
            {
                if (!help)
                {
                    ayudaPage.Visibility = Visibility.Visible;
                    main.Visibility = Visibility.Hidden;
                    webView.Visibility = Visibility.Hidden;
                    help = true;
                }
                else
                {
                    ayudaPage.Visibility = Visibility.Hidden;
                    help = false;
                    main.Visibility = Visibility.Visible; 
                }
            }

        }
    }
}