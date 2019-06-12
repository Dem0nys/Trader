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
        int Id;
        public WindowStore(string Name,string Money,int id)
        {
            InitializeComponent();
            Username.Content = Name;
            Mon.Content = Money;
            Id = id;
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

        private void ButtonBuy_Click(object sender, RoutedEventArgs e)
        {
            int id_skin = listViewCS.SelectedIndex+1;
            string Name= "Usp-s Kill Confirmed", ImgName= "CS_GO/ usp_s / kill_confirmed.png", Price="125.78";
            string connString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connString))
                {
                    conn.Open();
                    ImageBrush image = new ImageBrush();

                    string command = "SELECT * FROM Skins ";
                    using (SQLiteCommand cmd = new SQLiteCommand(command, conn))
                    {
                        
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                           
                            while (reader.Read())
                            {


                                if (reader["Id"].ToString() == id_skin.ToString())
                                {
                                    Name = reader["Name"].ToString();
                                    ImgName = reader["ImgName"].ToString();
                                    Price = reader["Price"].ToString();
                                }
                                //cmd.ExecuteNonQuery();
                            }
                            
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            connString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connString))
                {
                    conn.Open();
                    string command = "INSERT INTO Skins (Name,ImgName,Price,user_id) VALUES(@Name,@ImgName,@Price,@user_id)";

                    using (SQLiteCommand cmd = new SQLiteCommand(command, conn))
                    {
                        cmd.Prepare();
                        
                       
                        
                        
                         cmd.Parameters.AddWithValue("@Name", Name);
                        cmd.Parameters.AddWithValue("@ImgName", ImgName);
                        cmd.Parameters.AddWithValue("@Price", Price);
                        cmd.Parameters.AddWithValue("@user_id", Id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
