using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for Suggestion.xaml
    /// </summary>
    public partial class Suggestion : Page
    {
        public Suggestion()
        {
            InitializeComponent();
        }

        private void Cancel_Add_Item_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Windows[1].Close();
        }

        private void Add_Item_Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(Item_Type.Text);
            string ConString = ConfigurationManager.ConnectionStrings["db_connection"].ConnectionString;
            string CmdString = string.Empty;
            if (Item_Type.SelectedIndex == 0)
            {
                using (SqlConnection con = new SqlConnection(ConString))
                {
                    CmdString = "SELECT id, exercicio, tipo, dificuldade, duracao FROM exercicios WHERE exercicio = @exercicio AND tipo = @tipo AND dificuldade = @dificuldade AND duracao = @duracao";
                    SqlCommand cmd = new SqlCommand(CmdString, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@exercicio", Exercise_Name.Text);
                    cmd.Parameters.AddWithValue("@tipo", Exercise_Type.Text);
                    cmd.Parameters.AddWithValue("@dificuldade", Difficulty.Text);
                    cmd.Parameters.AddWithValue("@duracao", Exercise_Duration.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Exercicios");
                    sda.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        CmdString = "INSERT INTO exercicios (exercicio, tipo, dificuldade, duracao) VALUES ( @exercicio, @tipo, @dificuldade, @duracao);";
                        SqlCommand cmd2 = new SqlCommand(CmdString, con);
                        cmd2.CommandType = CommandType.Text;
                        cmd2.Parameters.AddWithValue("@exercicio", Exercise_Name.Text);
                        cmd2.Parameters.AddWithValue("@tipo", Exercise_Type.Text);
                        cmd2.Parameters.AddWithValue("@dificuldade", Difficulty.Text);
                        cmd2.Parameters.AddWithValue("@duracao", Exercise_Duration.Text);
                        con.Open();
                        cmd2.ExecuteNonQuery();
                        Application.Current.Windows[1].Close();
                    }
                    else
                    {
                        Add_Item_Error_Label.Content = "Exercise is available in the Exercises List!";
                        Add_Item_Error_Label.Visibility = Visibility.Visible;
                    }
                }
            }
            else
            {
                using (SqlConnection con = new SqlConnection(ConString))
                {
                    CmdString = "SELECT id, receita, tipo, dificuldade, calorias FROM receitas WHERE receita = @receita AND tipo = @tipo AND dificuldade = @dificuldade AND calorias = @calorias";
                    SqlCommand cmd = new SqlCommand(CmdString, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@receita", Receipt_Name.Text);
                    cmd.Parameters.AddWithValue("@tipo", Receipt_Type.Text);
                    cmd.Parameters.AddWithValue("@dificuldade", Difficulty.Text);
                    cmd.Parameters.AddWithValue("@calorias", Receipt_Calories.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Receitas");
                    sda.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        CmdString = "INSERT INTO receitas (receita, tipo, dificuldade, calorias) VALUES ( @receita, @tipo, @dificuldade, @calorias);";
                        SqlCommand cmd2 = new SqlCommand(CmdString, con);
                        cmd2.CommandType = CommandType.Text;
                        cmd2.Parameters.AddWithValue("@receita", Receipt_Name.Text);
                        cmd2.Parameters.AddWithValue("@tipo", Receipt_Type.Text);
                        cmd2.Parameters.AddWithValue("@dificuldade", Difficulty.Text);
                        cmd2.Parameters.AddWithValue("@calorias", Receipt_Calories.Text);
                        con.Open();
                        cmd2.ExecuteNonQuery();
                        Application.Current.Windows[1].Close();
                    }
                    else
                    {
                        Add_Item_Error_Label.Content = "Receipt is available in the Receipts List!";
                        Add_Item_Error_Label.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void Item_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Add_Item_Error_Label.Visibility = Visibility.Hidden;
            if (Item_Type.SelectedIndex == 0)
            {
                Receipt_Name.Visibility = Visibility.Hidden;
                Receipt_Type_Label.Visibility = Visibility.Hidden;
                Receipt_Type.Visibility = Visibility.Hidden;
                Receipt_Calories_Label.Visibility = Visibility.Hidden;
                Receipt_Calories.Visibility = Visibility.Hidden;
                Exercise_Receipt_Name_Label.Visibility = Visibility.Visible;
                Exercise_Name.Visibility = Visibility.Visible;
                Exercise_Type_Label.Visibility = Visibility.Visible;
                Exercise_Type.Visibility = Visibility.Visible;
                Difficulty_Label.Visibility = Visibility.Visible;
                Difficulty.Visibility = Visibility.Visible;
                Exercise_Duration_Label.Visibility = Visibility.Visible;
                Exercise_Duration.Visibility = Visibility.Visible;
            }
            else
            {
                Exercise_Name.Visibility = Visibility.Hidden;
                Exercise_Type_Label.Visibility = Visibility.Hidden;
                Exercise_Type.Visibility = Visibility.Hidden;
                Exercise_Duration_Label.Visibility = Visibility.Hidden;
                Exercise_Duration.Visibility = Visibility.Hidden;
                Exercise_Receipt_Name_Label.Visibility = Visibility.Visible;
                Receipt_Name.Visibility = Visibility.Visible;
                Receipt_Type_Label.Visibility = Visibility.Visible;
                Receipt_Type.Visibility = Visibility.Visible;
                Difficulty_Label.Visibility = Visibility.Visible;
                Difficulty.Visibility = Visibility.Visible;
                Receipt_Calories_Label.Visibility = Visibility.Visible;
                Receipt_Calories.Visibility = Visibility.Visible;
            }
            Add_Item_Button.Visibility = Visibility.Visible;
        }
    }
}
