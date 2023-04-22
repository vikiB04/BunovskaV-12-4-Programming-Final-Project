using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Windows.Shapes;

namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        public Search()
        {
            InitializeComponent();
        }

        private void Home_search_button_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            MainWindow mw = new MainWindow();
            home.welcome_textblock.Text = "Are you looking for something else?";
            home.Show();
            this.Close();
        }

        private void Bunovski_search_button_Click(object sender, RoutedEventArgs e)
        {
            Bunovski bunovski = new Bunovski();
            bunovski.Show();
            this.Close();

        }

        private void Milichini_search_button_Click(object sender, RoutedEventArgs e)
        {
            Milichini milichini = new Milichini();
            milichini.Show();
            this.Close();
        }

        private void Bundevi_search_button_Click(object sender, RoutedEventArgs e)
        {
            Bundevi bundevi = new Bundevi();
            bundevi.Show();
            this.Close();
        }

        private void Search_search_button_Click(object sender, RoutedEventArgs e)
        {
            Search search = new Search();
            search.Show();
            this.Close();
        }

        private void SeeAll_search_button_Click(object sender, RoutedEventArgs e)
        {
            SeeAll s = new SeeAll();
            s.Show();
            this.Close();

            SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-AAOO2UI;Initial Catalog=Final_Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            try
            {
                sqlCon.Open();
                string query = "Select Product_Name, Quantity, Price, Store, 'Milichini' as 'Family' from Milichini union select Product_Name, Quantity, Price, Store, 'Bunovski' as 'Family' from Bunovski union select   Product_Name, Quantity, Price, Store, 'Bundevi' as 'Family' from Bundevi"
                    ;

                SqlCommand cmd = new SqlCommand(query, sqlCon);

                cmd.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                s.AllList_DataGrid.ItemsSource = dt.DefaultView;
                adapter.Update(dt);

                sqlCon.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void LogOut_search_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Delete_bundevipage_button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-AAOO2UI;Initial Catalog=Final_Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            try
            {

                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

                string query = "SELECT COUNT(*) FROM Bunovski Where Product_Name=@Product_Name  union select count(*) from Bundevi where  Product_Name=@Product_Name union select count(*) from Milichini where Product_Name=@Product_Name ";

                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Product_Name", Search_searchpage_textbox.Text);

                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (!(count == 1))
                {
                    try
                    {
                        string query2 = "Select Product_Name, Quantity, Price, Store, 'Milichini' as 'Family' from Milichini where Product_Name = '"+Search_searchpage_textbox.Text+ "' union select Product_Name, Quantity, Price, Store, 'Bunovski' as 'Family' from Bunovski  where Product_Name = '"+Search_searchpage_textbox.Text+"' union select   Product_Name, Quantity, Price, Store, 'Bundevi' as 'Family' from Bundevi  where Product_Name = '"+Search_searchpage_textbox.Text+"'";

                        SqlCommand cmd = new SqlCommand(query2, sqlCon);

                        cmd.ExecuteNonQuery();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        Searchlist_searchpage_DataGrid.ItemsSource = dt.DefaultView;
                        adapter.Update(dt);

                        sqlCon.Close();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("No such product");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void Search_searchpage_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
