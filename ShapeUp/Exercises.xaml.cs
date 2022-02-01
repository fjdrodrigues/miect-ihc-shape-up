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
    /// Interaction logic for Exercises.xaml
    /// </summary>
    public partial class Exercises : Page
    {
        
        public Exercises()
        {
            InitializeComponent();

            FillDataGrid();
            
        }
        

        private void FillDataGrid()
        {
            string ConString = ConfigurationManager.ConnectionStrings["db_connection"].ConnectionString;
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString = "SELECT id, exercicio, tipo, dificuldade, duracao FROM exercicios";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Exercicios");
                sda.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    Empty_DataGrid_Label.Visibility = Visibility.Visible;
                    Exercises_DataGrid.Visibility = Visibility.Hidden;
                }
                else
                {
                    Empty_DataGrid_Label.Visibility = Visibility.Hidden;
                    Exercises_DataGrid.ItemsSource = dt.DefaultView;
                    Exercises_DataGrid.Visibility = Visibility.Visible;
                }
            }
        }

        private void AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();

            //Cancel the column you don't want to generate
            if (headername == "id")
            {
                e.Cancel = true;
            }else
            {
                e.Column.Header = headername.ElementAt(0).ToString().ToUpper()+headername.Substring(1);
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

        private void Account_Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Account());
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

        private void AddExercise_MyShapeUp_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Exercises_DataGrid.SelectedItem == null)
            {
                Adding_Exercise_To_MyShapeUp_Error.Content = "No Exercise Selected!";
                Adding_Exercise_To_MyShapeUp_Error.Visibility = Visibility.Visible;
            }
            else
            {
                DataRowView rowView = (DataRowView)Exercises_DataGrid.SelectedItem;
                DataRow row = rowView.Row;
                if (Is_Exercise_In_MyShapeUp(row))
                {
                    Adding_Exercise_To_MyShapeUp_Error.Content = "Exercise already in My ShapeUp!";
                    Adding_Exercise_To_MyShapeUp_Error.Visibility = Visibility.Visible;
                }
                else
                { 
                    Add_Exercise_MyShapeUp(row);
                    Adding_Exercise_To_MyShapeUp_Confirmation.Visibility = Visibility.Visible;
                }

            }
        }

        private Boolean Is_Exercise_In_MyShapeUp(DataRow row)
        {
            Object[] obj = (Object[])row.ItemArray;
            int id = (int)obj[0];
            string ConString = ConfigurationManager.ConnectionStrings["db_connection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("SELECT utilizador, exercicio FROM myExercise WHERE utilizador=@username AND exercicio=@exercise_id");
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@username", (String)Application.Current.Properties["user"]);
                cmd.Parameters.AddWithValue("@exercise_id", id);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Exercicios");
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                    return true;
            }
            return false;
        }

        private void Add_Exercise_MyShapeUp(DataRow row)
        {
            Object[] obj = (Object[])row.ItemArray;
            int id = (int)obj[0];
            string ConString = ConfigurationManager.ConnectionStrings["db_connection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO myExercise (utilizador, exercicio) VALUES(@username, @exercise_id);");
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@username", (String)Application.Current.Properties["user"]);
                cmd.Parameters.AddWithValue("@exercise_id", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void Suggest_Exercise_Button_Click(object sender, RoutedEventArgs e)
        {
            Pop_Up_Window pop = new Pop_Up_Window();
            pop.PopUp_Frame.Source = new Uri("Suggestion.xaml", UriKind.Relative);
            pop.Closed += new EventHandler(Pop_Up_Window_Exercise_Closed);
            pop.Show();
        }

        private void Pop_Up_Window_Exercise_Closed(object sender, EventArgs e)
        {
            FillDataGrid();
        }

        private void Exercises_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Adding_Exercise_To_MyShapeUp_Error.Visibility = Visibility.Hidden;
            Adding_Exercise_To_MyShapeUp_Confirmation.Visibility = Visibility.Hidden;
        }
    }
}
