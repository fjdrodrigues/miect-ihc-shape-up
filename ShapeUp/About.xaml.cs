using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShapeUp
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Page
    {
        public About()
        {
            InitializeComponent();
            if ((Boolean)Application.Current.Properties["logged"])
            {
                Logged_Menu.Visibility = Visibility.Visible;
                Not_Logged_Menu.Visibility = Visibility.Hidden;
            }
            else
            {
                Not_Logged_Menu.Visibility = Visibility.Visible;
                Logged_Menu.Visibility = Visibility.Hidden;
            }
        }


        private void Minimize_Button_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);

            window.WindowState = WindowState.Minimized;
        }

        private void Home_Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Home());
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Login());
        }

        private void Account_Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Account());
        }

        private void Exercises_Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Exercises());
        }

        private void Food_Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Food());
        }

        private void MyShapeUp_Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MyShapeUp());
        }

        

        private void Logout_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["user"] = "";
            Application.Current.Properties["logged"] = false;
            this.NavigationService.Navigate(new Home());
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
