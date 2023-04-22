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
    /// Interaction logic for Milichini.xaml
    /// </summary>
    public partial class Milichini : Window
    {
        public Milichini()
        {
            InitializeComponent();
        }

        private void Home_milichinipage_button_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            MainWindow mw = new MainWindow();
            home.welcome_textblock.Text = "Are you looking for something else?";
            home.Show();
            this.Close();
        }

        private void Bunovski_milichinipage_button_Click(object sender, RoutedEventArgs e)
        {
            Bunovski bunovski = new Bunovski();
            bunovski.Show();
            this.Close();
        }

        private void Milichini_milichinipage_button_Click(object sender, RoutedEventArgs e)
        {
            Milichini milichini = new Milichini();
            milichini.Show();
            this.Close();
        }

        private void Bundevi_milichinipage_button_Click(object sender, RoutedEventArgs e)
        {
            Bundevi bundevi = new Bundevi();
            bundevi.Show();
            this.Close();
        }

        private void Search_milichinipage_button_Click(object sender, RoutedEventArgs e)
        {
            Search search = new Search();
            search.Show();
            this.Close();
        }

        private void SeeAll_milichinipage_button_Click(object sender, RoutedEventArgs e)
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

        private void LogOut_milichinipage_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Add_Milichinipage_button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-AAOO2UI;Initial Catalog=Final_Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            try
            {

                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

                int qunatityconvert = int.Parse(Quantity_Add_Milichinipage_textbox.Text);
                float priceconvert = float.Parse(Price_Add_Milichinipage_textbox.Text);

                Type quantity = qunatityconvert.GetType();
                Type price = priceconvert.GetType();

                if (quantity.Equals(typeof(int)) && price.Equals(typeof(float)) && !(ProductName_Add_Milichinipage_textbox.Text.Contains("''")))
                {
                    string query = "Insert into Milichini (Product_Name, Quantity, Price, Store) values ('" + this.ProductName_Add_Milichinipage_textbox.Text + "', '" + this.Quantity_Add_Milichinipage_textbox.Text + "', '" + this.Quantity_Add_Milichinipage_textbox.Text + "', '" + this.Price_Add_Milichinipage_textbox.Text + "')";

                    SqlCommand cmd = new SqlCommand(query, sqlCon);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Successfully added product!");

                }
                else
                {
                    MessageBox.Show("The product's name, price or quantity are not written in the correct form. Hover the mouse over the add button for help.");
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void Update_Milichinipage_button_Copy_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-AAOO2UI;Initial Catalog=Final_Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            try
            {
                int qunatityconvert = int.Parse(Quantity_Add_Milichinipage_textbox.Text);

                Type quantity = qunatityconvert.GetType();

                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                if (quantity.Equals(typeof(int)))
                {

                    string query3 = "Update Milichini set Quantity=  '" + this.Quantity_Update_Milichinipage_textbox.Text + "' where Product_Name = '" + this.Product_Update_Milichinipage_combobox.Text + "' ";

                    SqlCommand cmd = new SqlCommand(query3, sqlCon);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Successfully updated product!");
                }
                else
                {
                    MessageBox.Show("Use whole numbers for quantity.");

                }





            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-AAOO2UI;Initial Catalog=Final_Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            try
            {

                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

                string query2 = "select Product_Name from Final_Project.dbo.Milichini";
                SqlCommand cmd2 = new SqlCommand(query2, sqlCon);

                SqlDataReader DR = cmd2.ExecuteReader();

                while (DR.Read())
                {
                    string name = DR.GetString(0);
                    Product_Update_Milichinipage_combobox.Items.Add(name);

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    

      

        private void SeeProducts_Milichinipage_button_Click(object sender, RoutedEventArgs e)
        {
            MilichiniAll milichiniAll = new MilichiniAll();
            milichiniAll.Show();
            this.Close();
            SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-AAOO2UI;Initial Catalog=Final_Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            try
            {
                sqlCon.Open();
                string query = "Select Product_Name, Quantity, Price, Store from Milichini";

                SqlCommand cmd = new SqlCommand(query, sqlCon);

                cmd.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                milichiniAll.MilichiniList_DataGrid.ItemsSource = dt.DefaultView;
                adapter.Update(dt);

                sqlCon.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Product_Delete_Milichinipage_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-AAOO2UI;Initial Catalog=Final_Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            try
            {

                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

                string query5 = "select Product_Name from Milichini";
                SqlCommand cmd2 = new SqlCommand(query5, sqlCon);

                SqlDataReader DR = cmd2.ExecuteReader();

                while (DR.Read())
                {
                    string name2 = DR.GetString(0);
                    Product_Delete_Milichinipage_combobox.Items.Add(name2);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Delete_Milichinipage_button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-AAOO2UI;Initial Catalog=Final_Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            try
            {

                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }


                string query4 = "Delete from Milichini where Product_Name=  '" + this.Product_Delete_Milichinipage_combobox.Text + "' ";

                SqlCommand cmd = new SqlCommand(query4, sqlCon);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully deleted product!");


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Quantity_Add_Milichinipage_textbox_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
