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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ShapeUp
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class Account : Page
    {
        public Account()
        {
            InitializeComponent();
            /*Get Info from DB*/
            GetUserData();
        }

        private void GetUserData()
        {
            string ConString = ConfigurationManager.ConnectionStrings["db_connection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM utilizador WHERE username=@username");
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@username", (String) Application.Current.Properties["user"]);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Exercicios");
                sda.Fill(dt);
                Name_Data_Label.Content = dt.Rows[0]["name"].ToString();
                Username_Data_Label.Content = dt.Rows[0]["username"].ToString();
                Email_Data_Label.Content = dt.Rows[0]["email"].ToString();
                DateTime bdate = (DateTime) dt.Rows[0]["dbirth"];
                Birthdate_Data_Label.Content = bdate.Date;

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
        
        private void About_Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new About());
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

        private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox p = (PasswordBox)sender;
            if (p.Password == "Password")
                p.Password = String.Empty;
        }

        private void Password_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox p = (PasswordBox)sender;
            if (p.Password == String.Empty)
                p.Password = "Password";
        }

        private void Edit_Password_Button_Click(object sender, RoutedEventArgs e)
        {
            string username = (string) Username_Data_Label.Content;
            string pass = Password_Box.Password;
            if (pass.Length < 8 || pass.Length > 32)
            {
                Bad_Password_Label.Content = "*Password length between 8 and 32 characters.";
            }else
            {
                /*Change pass in DB*/
                string ConString = ConfigurationManager.ConnectionStrings["db_connection"].ConnectionString;
                try
                {
                    using (SqlConnection con = new SqlConnection(ConString))
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE utilizador SET passw=@passw WHERE username=@username");
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@passw", pass);
                        cmd.Parameters.AddWithValue("@username", username);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
