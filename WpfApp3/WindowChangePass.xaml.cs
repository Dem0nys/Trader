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
    /// Interaction logic for WindowChangePass.xaml
    /// </summary>
    public partial class WindowChangePass : Window
    {
        int Id;
        public WindowChangePass(string id)
        {
            Id = int.Parse(id);
            InitializeComponent();
            //UPDATE users SET role = 99 WHERE name = 'Fred'

            
        }
        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            string connString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connString))
                {
                    conn.Open();
                    string command = "UPDATE Users SET Password = @Password WHERE Id = @Id";

                    using (SQLiteCommand cmd = new SQLiteCommand(command, conn))
                    {
                        
                       
                        if (!String.IsNullOrEmpty(passPass.Password))
                        {

                            if (!String.IsNullOrEmpty(passRepPass.Password) && passRepPass.Password == passPass.Password)
                                cmd.Parameters.AddWithValue("@Password", passPass.Password);
                            else
                            {
                                MessageBox.Show("Repeated password isn`t correct");
                                return;
                            }
                        }
                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.Prepare();
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
