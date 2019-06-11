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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public string nam;
        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.ShowDialog();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            string id = string.Empty;
            string name = string.Empty;
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

                                if (txtLogin.Text == reader["Email"].ToString())
                                {
                                    if (passPass.Password == reader["Password"].ToString())
                                    {
                                        id = reader["Id"].ToString();
                                        name = reader["Name"].ToString();
                                        
                                        
                                        
                                    }
                                }
                            }
                        }
                    }
                    conn.Close();
                }
                if (id != string.Empty)
                {
                    
                    Main window = new Main(name,id);
                    window.ShowDialog();
                    this.Close();
                }
            }
            
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonForgot_Click(object sender, RoutedEventArgs e)
        {
            WindowForgot window = new WindowForgot();
            window.ShowDialog();
        }

        private void TxtLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            nam = txtLogin.Text;
        }
    }
}
