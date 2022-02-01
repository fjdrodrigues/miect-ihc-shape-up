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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Minimize_Button_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            
            window.WindowState = WindowState.Minimized;
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Home_Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Home());
        }

        private void Login_Menu_Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Login());
        }
        
        private void About_Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new About());
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            Bad_Login_Label.Content = "";
            /*Check Username and Password in DB*/
            String username = Username.Text;
            String pass = Password.Password;

            if (!existUser(username))
            {
                Bad_Login_Label.Content = "*Username not Registered";
            }
            else if(!correctPassword(username, pass))
            {
                Bad_Login_Label.Content = "*Wrong Password";
            }
            else
            {
                Application.Current.Properties["user"] = username;
                Application.Current.Properties["logged"] = true;
                this.NavigationService.Navigate(new Home());
            }
            
        }
        
        private Boolean existUser(string username)
        {
            string ConString = ConfigurationManager.ConnectionStrings["db_connection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM utilizador WHERE username=@username");
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@username", username);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Exercicios");
                sda.Fill(dt);
                if(dt.Rows.Count == 0)
                {
                    return false;
                }
                else if(dt.Rows[0]["username"].ToString() == username)
                {
                    return true;
                }

            }
            return false;
        }

        private Boolean correctPassword(string username, string pass)
        {
            string ConString = ConfigurationManager.ConnectionStrings["db_connection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM utilizador WHERE username=@username");
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@username", username);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Exercicios");
                sda.Fill(dt);
                if (dt.Rows[0]["username"].ToString() == username && dt.Rows[0]["passw"].ToString() == pass)
                {
                        return true;
                }
            }
            return false;
        }

        private void Username_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            if(t.Text == "Username")
                t.Text = String.Empty;
        }
        

        private void Username_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (t.Text == String.Empty)
                t.Text = "Username";
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

        private void Name_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (t.Text == "Name")
                t.Text = String.Empty;
        }
        
        private void Name_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (t.Text == String.Empty)
                t.Text = "Name";
        }

        private void Email_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (t.Text == "Email")
                t.Text = String.Empty;
        }


        private void Email_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (t.Text == String.Empty)
                t.Text = "Email";
        }

        private void Forgot_Password_Click(object sender, RoutedEventArgs e)
        {
            Login_Grid.Visibility = Visibility.Hidden;
            Password_Recovery_Grid.Visibility = Visibility.Visible;

        }

        private void New_User_Click(object sender, RoutedEventArgs e)
        {
            Login_Grid.Visibility = Visibility.Hidden;
            Register_Grid.Visibility = Visibility.Visible;
        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            Register_Error_Label.Content = "";
            String name = Register_Name.Text;
            String username = Register_Username.Text;
            String email = Register_Email.Text;
            String pass = Register_Password.Password;
            DateTime birthdate = (DateTime) Register_Birthdate.SelectedDate;

            if (name.Length > 50)
            {
                Register_Error_Label.Content = "*Name length shorter than 50 Characters.";
            }
            else if (username.Length < 6 || username.Length > 50)
            {
                Register_Error_Label.Content = "*Username length between 6 and 50 Characters.";
            }
            else if (!email.Contains("@"))
            {
                Register_Error_Label.Content = "*Insert a valid email.";
            }
            else if (pass.Length < 8 || pass.Length > 32)
            {
                Register_Error_Label.Content = "*Password length between 8 and 32 Characters.";
            }
            else if (birthdate > DateTime.Now)
            {
                Register_Error_Label.Content = "*Invalid Birthdate.";
            }else
            {
                /*Register User*/
                string ConString = ConfigurationManager.ConnectionStrings["db_connection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(ConString))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO utilizador (username, name, email, passw, dbirth) VALUES(@username, @name, @email, @passw, @dbirth);");
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@passw", pass);
                    cmd.Parameters.AddWithValue("@dbirth", birthdate.Date);
                    
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                Application.Current.Properties["user"] = username;
                Application.Current.Properties["logged"] = true;
                this.NavigationService.Navigate(new Home());
            }
            
        }

        private void Cancel_Register_Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Login());
        }

        private void Password_Recover_Button_Click(object sender, RoutedEventArgs e)
        {
            /*Search email*/
            /*
            if (!found)
                Wrong_Email_Label.Content = "*Wrong Email.";
            else
                this.NavigationService.Navigate(new Login());
            */
        }

        
    }
}
