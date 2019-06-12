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
    /// Interaction logic for WindowStore.xaml
    /// </summary>
    public partial class WindowStore : Window
    {
        public WindowStore(string Name,string Money)
        {
            InitializeComponent();
            Username.Content = Name;
            Mon.Content = Money;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonCs_Click(object sender, RoutedEventArgs e)
        {
            borderSkinsDota.Visibility = System.Windows.Visibility.Hidden;
            borderAdvert.Visibility = System.Windows.Visibility.Hidden;
            borderSkins.Visibility= System.Windows.Visibility.Hidden;
            string connString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connString))
                {
                    conn.Open();
                    ImageBrush image = new ImageBrush();
                    
                    string command = "SELECT * FROM SkinsCS";
                    using (SQLiteCommand cmd = new SQLiteCommand(command, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            List<SkinItem> skins = new List<SkinItem>();
                            while (reader.Read())
                            {
                                SkinItem skin = new SkinItem();
                                skin.Id = int.Parse(reader["Id"].ToString());
                                skin.Name = reader["Name"].ToString();
                                skin.Image = new BitmapImage(new Uri("../../"+reader["ImgName"].ToString(), UriKind.Relative));
                                skin.Price = double.Parse(reader["Price"].ToString());
                                skins.Add(skin);
                               
                                
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
            
        
            borderSkinsCS.Visibility = System.Windows.Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonDota_Click(object sender, RoutedEventArgs e)
        {
            borderSkinsCS.Visibility = System.Windows.Visibility.Hidden;
            borderAdvert.Visibility = System.Windows.Visibility.Hidden;
            borderSkins.Visibility = System.Windows.Visibility.Hidden;
            string connString = ConfigurationManager.ConnectionStrings["BConnection"].ConnectionString;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connString))
                {
                    conn.Open();
                    ImageBrush image = new ImageBrush();

                    string command = "SELECT * FROM SkinsDota";
                    using (SQLiteCommand cmd = new SQLiteCommand(command, conn))
                    {
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
                                
                            }
                            listViewDota.ItemsSource = skins;
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            borderSkinsDota.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
