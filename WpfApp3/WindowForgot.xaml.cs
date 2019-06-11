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
    /// Interaction logic for WindowForgot.xaml
    /// </summary>
    public partial class WindowForgot : Window
    {
        public WindowForgot()
        {
            InitializeComponent();
            
        }
        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            string  id = string.Empty;
            string connString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connString))
                {
                    conn.Open();
                    string command = "SELECT * FROM Users";
                    using (SQLiteCommand cmd = new SQLiteCommand(command, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                if (txtEmail.Text == reader["Email"].ToString())
                                {
                                    if (txtName.Text == reader["Name"].ToString())
                                    {
                                        id = reader["Id"].ToString();


                                    }
                                }
                            }
                        }
                    }
                    conn.Close();
                }
                if (id != string.Empty)
                {
                    WindowChangePass window = new WindowChangePass(id);
                    window.ShowDialog();
                    this.Close();
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
