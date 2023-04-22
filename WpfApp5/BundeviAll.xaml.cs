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
    /// Interaction logic for BundeviAll.xaml
    /// </summary>
    public partial class BundeviAll : Window
    {
        public BundeviAll()
        {
            InitializeComponent();
        }

        private void Home_bundevilist_button_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            MainWindow mw = new MainWindow();
            home.welcome_textblock.Text = "Are you looking for something else?";
            home.Show();
            this.Close();
        }

        private void Bunovski_bundevilist_button_Click(object sender, RoutedEventArgs e)
        {
            Bunovski bunovski = new Bunovski();
            bunovski.Show();
            this.Close();
        }

        private void Milichini_bundevilist_button_Click(object sender, RoutedEventArgs e)
        {

            Milichini milichini = new Milichini();
            milichini.Show();
            this.Close();
        }

        private void Bundevi_bundevilist_button_Click(object sender, RoutedEventArgs e)
        {
            Bundevi bundevi = new Bundevi();
            bundevi.Show();
            this.Close();
        }

        private void Search_bundevilist_button_Click(object sender, RoutedEventArgs e)
        {

            Search search = new Search();
            search.Show();
            this.Close();
        }

        private void SeeAll_bundevilist_button_Click(object sender, RoutedEventArgs e)
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

        private void LogOut_bundevilist_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Back_bundevilist_button_Click(object sender, RoutedEventArgs e)
        {
            Bundevi bundevi = new Bundevi();
            bundevi.Show();
            this.Close();
        }
    }
}
