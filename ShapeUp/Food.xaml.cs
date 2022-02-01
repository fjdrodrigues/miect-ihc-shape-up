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
    /// Interaction logic for Food.xaml
    /// </summary>
    public partial class Food : Page
    {
        public Food()
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
                CmdString = "SELECT id, receita, tipo, dificuldade, calorias FROM receitas";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Receitas");
                sda.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    Empty_DataGrid_Label.Visibility = Visibility.Visible;
                    Food_DataGrid.Visibility = Visibility.Hidden;
                }
                else
                {
                    Empty_DataGrid_Label.Visibility = Visibility.Hidden;
                    Food_DataGrid.ItemsSource = dt.DefaultView;
                    Food_DataGrid.Visibility = Visibility.Visible;
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
            }
            else
            {
                e.Column.Header = headername.ElementAt(0).ToString().ToUpper() + headername.Substring(1);
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

        private void Exercises_Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Exercises());
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

        private void AddReceipt_MyShapeUp_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Food_DataGrid.SelectedItem == null)
            {
                Adding_Receipt_To_MyShapeUp_Error.Content = "No Receipt Selected!";
                Adding_Receipt_To_MyShapeUp_Error.Visibility = Visibility.Visible;
            }
            else
            {
                DataRowView rowView = (DataRowView)Food_DataGrid.SelectedItem;
                DataRow row = rowView.Row;
                if (Is_Receipt_In_MyShapeUp(row))
                {
                    Adding_Receipt_To_MyShapeUp_Error.Content = "Receipt already in My ShapeUp!";
                    Adding_Receipt_To_MyShapeUp_Error.Visibility = Visibility.Visible;
                }
                else
                { 
                    Add_Receipt_MyShapeUp(row);
                    Adding_Receipt_To_MyShapeUp_Confirmation.Visibility = Visibility.Visible;
                }
                    
            }
        }

        private Boolean Is_Receipt_In_MyShapeUp(DataRow row)
        {
            Object[] obj = (Object[])row.ItemArray;
            int id = (int)obj[0];
            string ConString = ConfigurationManager.ConnectionStrings["db_connection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("SELECT utilizador, receita FROM myFood WHERE utilizador=@username AND receita=@receipt_id");
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@username", (String)Application.Current.Properties["user"]);
                cmd.Parameters.AddWithValue("@receipt_id", id);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Receitas");
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                    return true;
            }
            return false;
        }

        private void Add_Receipt_MyShapeUp(DataRow row)
        {
            Object[] obj = (Object[])row.ItemArray;
            int id = (int)obj[0];
            string ConString = ConfigurationManager.ConnectionStrings["db_connection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO myFood (utilizador, receita) VALUES(@username, @receipt_id);");
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@username", (String)Application.Current.Properties["user"]);
                cmd.Parameters.AddWithValue("@receipt_id", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void Suggest_Receipt_Button_Click(object sender, RoutedEventArgs e)
        {
            Pop_Up_Window pop = new Pop_Up_Window();
            pop.PopUp_Frame.Source = new Uri("Suggestion.xaml", UriKind.Relative);
            pop.Closed += new EventHandler(Pop_Up_Window_Food_Closed);
            pop.Show();
        }

        private void Pop_Up_Window_Food_Closed(object sender, EventArgs e)
        {
            FillDataGrid();
        }

        private void Food_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Adding_Receipt_To_MyShapeUp_Error.Visibility = Visibility.Hidden;
            Adding_Receipt_To_MyShapeUp_Confirmation.Visibility = Visibility.Hidden;
        }
    }
}
