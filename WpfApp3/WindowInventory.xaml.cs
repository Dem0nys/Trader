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
using WpfApp3.Models;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for WindowInventory.xaml
    /// </summary>
    public partial class WindowInventory : Window
    {
        int Id;
        public WindowInventory(string name, string id,string money)
        {
            InitializeComponent();
            Username.Content = name;
            Mon.Content = money;
            Id = int.Parse(id);
            string connString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connString))
                {
                    conn.Open();
                    ImageBrush image = new ImageBrush();

                    string command = "SELECT * FROM Skins WHERE user_id = @Id";
                    using (SQLiteCommand cmd = new SQLiteCommand(command, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id",Id);
                        
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            List<SkinItem> skins = new List<SkinItem>();
                            while (reader.Read())
                            {
                                
                                SkinItem skin = new SkinItem();
                                skin.Id = int.Parse(reader["Id"].ToString());
                                skin.Name = reader["Name"].ToString();
                                skin.Image = new BitmapImage(new Uri("../../" + reader["ImgName"].ToString(), UriKind.Relative));
                                skin.Price = double.Parse(reader["Price"].ToString());
                                skins.Add(skin);

                               //cmd.ExecuteNonQuery();
                            }
                            listViewCS.ItemsSource = skins;
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
