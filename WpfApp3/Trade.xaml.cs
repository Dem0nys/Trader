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
    /// Interaction logic for Trade.xaml
    /// </summary>
    public partial class Trade : Window
    {
        List<SkinItem> skins = new List<SkinItem>();
        List<SkinItem> skins1 = new List<SkinItem>();
        List<SkinItem> skins2 = new List<SkinItem>();
        public Trade(object name,object money, int Id)
        { 
            InitializeComponent();
            Mon.Content = money;
            Username.Content = name;
            add(Id);
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
                                skin.Image = new BitmapImage(new Uri("../../" + reader["ImgName"].ToString(), UriKind.Relative));
                                skin.Price = double.Parse(reader["Price"].ToString());
                                skins.Add(skin);
                            }
                            listViewCS.ItemsSource = skins;
                            listViewCS.Items.Refresh();
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ButtonCs_Click(object sender, RoutedEventArgs e)
        {
            borderSkinsDota.Visibility = System.Windows.Visibility.Hidden;
            borderSkins.Visibility = System.Windows.Visibility.Hidden;
            borderSkinsDotaAdd.Visibility = System.Windows.Visibility.Hidden;
            border.Visibility = System.Windows.Visibility.Hidden;
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
                                skin.Image = new BitmapImage(new Uri("../../" + reader["ImgName"].ToString(), UriKind.Relative));
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

            borderSkinsCSAdd.Visibility = System.Windows.Visibility.Visible;
            borderSkinsCS.Visibility = System.Windows.Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonDota_Click(object sender, RoutedEventArgs e)
        {
            borderSkinsCS.Visibility = System.Windows.Visibility.Hidden;
            borderSkins.Visibility = System.Windows.Visibility.Hidden;
            borderSkinsCSAdd.Visibility = System.Windows.Visibility.Hidden;
            border.Visibility = System.Windows.Visibility.Hidden;
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
            borderSkinsDotaAdd.Visibility = System.Windows.Visibility.Visible;
            borderSkinsDota.Visibility = System.Windows.Visibility.Visible;
        }

        public void add(int id)
        {
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
                                combo.Items.Add(reader["Id"].ToString());    
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StackPanel_GotFocus(object sender, RoutedEventArgs e)
        {
                int id_skin = listViewCS.SelectedIndex + 1;
                double num;
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
                                while (reader.Read())
                                {
                                    if (reader["Id"].ToString() == id_skin.ToString())
                                    {
                                        SkinItem skin = new SkinItem();
                                        num = Math.Round(double.Parse(reader["Price"].ToString()));
                                        skin.Id = id_skin;
                                        skin.Name = reader["Name"].ToString();
                                        skin.Image = new BitmapImage(new Uri("../../" + reader["ImgName"].ToString(), UriKind.Relative));
                                        skin.Price = int.Parse(num.ToString());
                                        skins1.Add(skin);
                                    }
                                    listViewCSAdd.ItemsSource = skins1;
                                    listViewCSAdd.Items.Refresh();
                                }

                            }
                        }
                    }
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        private void StackPanel_GotFocus_1(object sender, RoutedEventArgs e)
        {
            int id_skin = listViewDota.SelectedIndex + 1;
            double num;
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
                            while (reader.Read())
                            {
                                if (reader["Id"].ToString() == id_skin.ToString())
                                {
                                    SkinItem skin = new SkinItem();
                                    num = Math.Round(double.Parse(reader["Price"].ToString()));
                                    skin.Id = id_skin;
                                    skin.Name = reader["Name"].ToString();
                                    skin.Image = new BitmapImage(new Uri("../../" + reader["ImgName"].ToString(), UriKind.Relative));
                                    skin.Price = int.Parse(num.ToString());
                                    skins2.Add(skin);
                                }
                                listViewDotaAdd.ItemsSource = skins2;
                                listViewDotaAdd.Items.Refresh();
                            }

                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            skins1.Clear();
            skins2.Clear();
            listViewDotaAdd.ItemsSource = skins2;
            listViewDotaAdd.Items.Refresh();
            listViewCSAdd.ItemsSource = skins1;
            listViewCSAdd.Items.Refresh();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int num = combo.SelectedIndex;
            num += 2;
            string connString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connString))
                {
                    conn.Open();
                    string command = "INSERT INTO Skins (Name,ImgName,Price,user_id) VALUES(@Name,@ImgName,@Price,@user_id)";

                    using (SQLiteCommand cmd = new SQLiteCommand(command, conn))
                    {
                        foreach (var c in skins1)
                        {
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@Name", c.Name);
                            cmd.Parameters.AddWithValue("@ImgName", c.Image);
                            cmd.Parameters.AddWithValue("@Price", c.Price);
                            cmd.Parameters.AddWithValue("@user_id", num);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
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
                        foreach (var c in skins2)
                        {
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@Name", c.Name);
                            cmd.Parameters.AddWithValue("@ImgName", c.Image);
                            cmd.Parameters.AddWithValue("@Price", c.Price);
                            cmd.Parameters.AddWithValue("@user_id", num);
                            cmd.ExecuteNonQuery();
                        }
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
