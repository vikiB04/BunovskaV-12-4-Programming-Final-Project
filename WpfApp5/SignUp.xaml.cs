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
using System.Windows.Markup;

namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void SignUp_signup_Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source = DESKTOP-AAOO2UI; Initial Catalog = Final_Project; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
               

                if (!(Email_SignUp_textbox.Text.Contains("@")))
                {
                    MessageBox.Show("Invalid email address!");

                }

                else
                {
                    string query2 = "SELECT COUNT(*) FROM Persons Where Username=@Username or Password=@Password";

                    SqlCommand cmd2 = new SqlCommand(query2, sqlCon);
                    cmd2.CommandType = CommandType.Text;
                    cmd2.Parameters.AddWithValue("@Username", Username_SignUp_textbox.Text);
                    cmd2.Parameters.AddWithValue("@Password", Password_SignUp_passwordbox.Password);
                    cmd2.Parameters.AddWithValue("@Email", Email_SignUp_textbox.Text);


                    int count = Convert.ToInt32(cmd2.ExecuteScalar());
                    if (count == 1)
                    {
                        MessageBox.Show("The user already exists! Try a different username or password.");
                    }
                    else
                    {
                        string query = "Insert into Persons (Username, Password, Email) values ('" + this.Username_SignUp_textbox.Text + "', '" + this.Password_SignUp_passwordbox.Password + "', '" + this.Email_SignUp_textbox.Text + "')";

                        SqlCommand cmd = new SqlCommand(query, sqlCon);

                        cmd.ExecuteNonQuery();

                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    } 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { sqlCon.Close(); }
        }
    }
}
