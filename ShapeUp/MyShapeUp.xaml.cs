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
    /// Interaction logic for MyShapeUp.xaml
    /// </summary>
    public partial class MyShapeUp : Page
    {
        public MyShapeUp()
        {
            InitializeComponent();
            FillExerciseDataGrid();
        }

        public void FillExerciseDataGrid()
        {
            string ConString = ConfigurationManager.ConnectionStrings["db_connection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("SELECT exercicios.id, exercicios.exercicio, exercicios.tipo, exercicios.dificuldade, exercicios.duracao"
                   +" FROM exercicios INNER JOIN myExercise ON exercicios.id=myExercise.exercicio WHERE myExercise.utilizador=@username");
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@username", (String)Application.Current.Properties["user"]);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Exercicios");
                sda.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    Empty_DataGrid_Label.Content = "No Exercises added to My ShapeUp.";
                    Empty_DataGrid_Label.Visibility = Visibility.Visible;
                    MyShapeUp_DataGrid.Visibility = Visibility.Hidden;
                    Delete_Exercise_Button.Visibility = Visibility.Hidden;
                }
                else
                {
                    Empty_DataGrid_Label.Visibility = Visibility.Hidden;
                    MyShapeUp_DataGrid.ItemsSource = dt.DefaultView;
                    MyShapeUp_DataGrid.Visibility = Visibility.Visible;
                }
            }
        }

        public void FillFoodDataGrid()
        {
            string ConString = ConfigurationManager.ConnectionStrings["db_connection"].ConnectionString;
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("SELECT receitas.id, receitas.receita, receitas.tipo, receitas.dificuldade, receitas.calorias"
                    + " FROM receitas INNER JOIN myFood ON receitas.id=myFood.receita WHERE myFood.utilizador=@username");
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@username", (String)Application.Current.Properties["user"]);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Receitas");
                sda.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    Empty_DataGrid_Label.Content = "No Receipts added to My ShapeUp.";
                    Empty_DataGrid_Label.Visibility = Visibility.Visible;
                    MyShapeUp_DataGrid.Visibility = Visibility.Hidden;
                    Delete_Food_Button.Visibility = Visibility.Hidden;
                }
                else
                {
                    Empty_DataGrid_Label.Visibility = Visibility.Hidden;
                    MyShapeUp_DataGrid.ItemsSource = dt.DefaultView;
                    MyShapeUp_DataGrid.Visibility = Visibility.Visible;
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

        private void Food_Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Food());
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

        private void Exercises_Grid_Button_Click(object sender, RoutedEventArgs e)
        {
            Add_Receipt_Button.Visibility = Visibility.Hidden;
            Delete_Food_Button.Visibility = Visibility.Hidden;
            Add_Exercise_Button.Visibility = Visibility.Visible;
            Delete_Exercise_Button.Visibility = Visibility.Visible;
            Deleting_Receipt_From_MyShapeUp_Error.Visibility = Visibility.Hidden;
            Deleting_Receipt_From_MyShapeUp_Confirmation.Visibility = Visibility.Hidden;
            Deleting_Exercise_From_MyShapeUp_Error.Visibility = Visibility.Hidden;
            Deleting_Exercise_From_MyShapeUp_Confirmation.Visibility = Visibility.Hidden;
            FillExerciseDataGrid();
        }

        private void Food_Grid_Button_Click(object sender, RoutedEventArgs e)
        {
            Add_Exercise_Button.Visibility = Visibility.Hidden;
            Delete_Exercise_Button.Visibility = Visibility.Hidden;
            Add_Receipt_Button.Visibility = Visibility.Visible;
            Delete_Food_Button.Visibility = Visibility.Visible;
            Deleting_Receipt_From_MyShapeUp_Error.Visibility = Visibility.Hidden;
            Deleting_Receipt_From_MyShapeUp_Confirmation.Visibility = Visibility.Hidden;
            Deleting_Exercise_From_MyShapeUp_Error.Visibility = Visibility.Hidden;
            Deleting_Exercise_From_MyShapeUp_Confirmation.Visibility = Visibility.Hidden;
            FillFoodDataGrid();
        }

        private void Delete_Food_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MyShapeUp_DataGrid.SelectedItem == null)
            {
                Deleting_Receipt_From_MyShapeUp_Error.Visibility = Visibility.Visible;
                Deleting_Receipt_From_MyShapeUp_Confirmation.Visibility = Visibility.Hidden;
                Deleting_Exercise_From_MyShapeUp_Error.Visibility = Visibility.Hidden;
                Deleting_Exercise_From_MyShapeUp_Confirmation.Visibility = Visibility.Hidden;
            }
            else
            {
                String username = (String)Application.Current.Properties["user"];

                DataRowView rowView = (DataRowView)MyShapeUp_DataGrid.SelectedItem;
                DataRow row = rowView.Row;
                Object[] obj = (Object[])row.ItemArray;
                int receipt_id = (int)obj[0];
                Console.WriteLine(receipt_id);
                string ConString = ConfigurationManager.ConnectionStrings["db_connection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(ConString))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM myFood WHERE utilizador=@username AND receita=@receipt_id");
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@receipt_id", receipt_id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                Deleting_Receipt_From_MyShapeUp_Error.Visibility = Visibility.Hidden;
                Deleting_Receipt_From_MyShapeUp_Confirmation.Visibility = Visibility.Visible;
                Deleting_Exercise_From_MyShapeUp_Error.Visibility = Visibility.Hidden;
                Deleting_Exercise_From_MyShapeUp_Confirmation.Visibility = Visibility.Hidden;
                FillFoodDataGrid();
            }
        }

        private void Delete_Exercise_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MyShapeUp_DataGrid.SelectedItem == null)
            {
                Deleting_Receipt_From_MyShapeUp_Error.Visibility = Visibility.Hidden;
                Deleting_Receipt_From_MyShapeUp_Confirmation.Visibility = Visibility.Hidden;
                Deleting_Exercise_From_MyShapeUp_Error.Visibility = Visibility.Visible;
                Deleting_Exercise_From_MyShapeUp_Confirmation.Visibility = Visibility.Hidden;
            }
            else
            {
                String username = (String)Application.Current.Properties["user"];

                DataRowView rowView = (DataRowView)MyShapeUp_DataGrid.SelectedItem;
                DataRow row = rowView.Row;
                Object[] obj = (Object[])row.ItemArray;
                int exercise_id = (int)obj[0];
                string ConString = ConfigurationManager.ConnectionStrings["db_connection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(ConString))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM myExercise WHERE utilizador=@username AND exercicio=@exercise_id");
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@exercise_id", exercise_id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                Deleting_Receipt_From_MyShapeUp_Error.Visibility = Visibility.Hidden;
                Deleting_Receipt_From_MyShapeUp_Confirmation.Visibility = Visibility.Hidden;
                Deleting_Exercise_From_MyShapeUp_Error.Visibility = Visibility.Hidden;
                Deleting_Exercise_From_MyShapeUp_Confirmation.Visibility = Visibility.Visible;
                FillExerciseDataGrid();
            }
        }

        private void Add_Receipt_Button_Click(object sender, RoutedEventArgs e)
        {
            Pop_Up_Window pop = new Pop_Up_Window();
            pop.PopUp_Frame.Source = new Uri("Add_Item.xaml", UriKind.Relative);
            pop.Closed += new EventHandler(Pop_Up_Window_Food_Closed);
            pop.Show();
        }

        

        private void Add_Exercise_Button_Click(object sender, RoutedEventArgs e)
        {
            Pop_Up_Window pop = new Pop_Up_Window();
            pop.PopUp_Frame.Source = new Uri("Add_Item.xaml", UriKind.Relative);
            pop.Closed += new EventHandler(Pop_Up_Window_Exercise_Closed);
            pop.Show();
        }

        private void Pop_Up_Window_Exercise_Closed(object sender, EventArgs e)
        {
            FillExerciseDataGrid();
        }

        private void Pop_Up_Window_Food_Closed(object sender, EventArgs e)
        {
            FillFoodDataGrid();
        }

        private void MyShapeUp_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Deleting_Receipt_From_MyShapeUp_Error.Visibility = Visibility.Hidden;
            Deleting_Receipt_From_MyShapeUp_Confirmation.Visibility = Visibility.Hidden;
            Deleting_Exercise_From_MyShapeUp_Error.Visibility = Visibility.Hidden;
            Deleting_Exercise_From_MyShapeUp_Confirmation.Visibility = Visibility.Hidden;
        }
    }
}
