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
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main(string name)
        {
            InitializeComponent();
            Username.Content = name;
        }
        void MyOperation()
        {
            //string connString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            //try
            //{
            //    using (SQLiteConnection conn = new SQLiteConnection(connString))
            //    {
            //        conn.Open();
            //        string command = "SELECT * FROM Users";
            //        using (SQLiteCommand cmd = new SQLiteCommand(command, conn))
            //        {
            //            using (SQLiteDataReader reader = cmd.ExecuteReader())
            //            {
            //                while (reader.Read())
            //                {
            //                    if (txtLogin.Text == reader["Name"].ToString())
            //                    {

            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (SQLiteException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

    }
}
