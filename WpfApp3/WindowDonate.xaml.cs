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
    /// Interaction logic for WindowDonate.xaml
    /// </summary>
    public partial class WindowDonate : Window
    {
        int Id;
        public int Money;
        public WindowDonate(string id)
        {
            InitializeComponent();
            Id = int.Parse(id);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonDonate_Click(object sender, RoutedEventArgs e)
        {
            string money = string.Empty;
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
                                if (Id.ToString() == reader["Id"].ToString())
                                {
                                        money = reader["Money"].ToString();                           
                                        
                                
                                }
                            }
                        }
                    }
                    conn.Close();
                }
                if (money != string.Empty)
                {
                    Money = int.Parse(money);
                    this.Close();
                }
            }

            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            connString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            int m = int.Parse(txtSum.Text) + Money;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connString))
                {
                    conn.Open();
                    string command = "UPDATE Users SET Money = @Money WHERE Id = @Id";

                    using (SQLiteCommand cmd = new SQLiteCommand(command, conn))
                    {


                        if (!String.IsNullOrEmpty(txtSum.Text))
                        {      
                            if(int.Parse(txtSum.Text)<50)
                                cmd.Parameters.AddWithValue("@Money",m.ToString() );                          
                        }
                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Imput normal sum !!");
            }
            this.Close();
        }
    }
}
