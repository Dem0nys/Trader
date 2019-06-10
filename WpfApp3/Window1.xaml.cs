using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
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

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            string connString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connString))
                {
                    conn.Open();
                    string command = "INSERT INTO Users (Email,Name,Password) VALUES(@Email,@Password,@Name,@DateOfBirth)";

                    using (SQLiteCommand cmd = new SQLiteCommand(command, conn))
                    {
                        cmd.Prepare();
                        if (!String.IsNullOrEmpty(txtEmail.Text))
                            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        else
                        {
                            MessageBox.Show("Enter login");
                            return;
                        }
                        if (!String.IsNullOrEmpty(passPass.Password))
                        {
                            
                            if (!String.IsNullOrEmpty(passRepPass.Password))
                                cmd.Parameters.AddWithValue("@Password", passPass.Password);
                            else
                            {
                                MessageBox.Show("Repeated password isn`t correct");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Enter password");
                            return;
                        }
                        if (!String.IsNullOrEmpty(txtName.Text))
                            cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        else
                        {
                            MessageBox.Show("Enter name");
                            return;
                        }                        
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }
    }
}
