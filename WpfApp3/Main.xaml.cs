﻿using System;
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
        int Id;
        public Main(string name,string id)
        {
            InitializeComponent();
            Username.Content = name;
            Id = int.Parse(id);
            oper();
        }
        public void oper()
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
                                if (Username.Content.ToString() == reader["Name"].ToString())
                                {
                                    Mon.Content = reader["Money"].ToString();
                                }
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

        }

        private void ButtonStore_Click(object sender, RoutedEventArgs e)
        {
            WindowStore store = new WindowStore(Username.Content.ToString(),Mon.Content.ToString(),Id);
            store.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonDonate_Click(object sender, RoutedEventArgs e)
        {
            WindowDonate window = new WindowDonate(Id.ToString());
            if (window.ShowDialog() == true)
            {
                 Mon.Content = window.Money;
            }
            oper();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonTrade_Click(object sender, RoutedEventArgs e)
        {
            if (Username.Content.ToString() == "Admin")
            {
                Trade trade = new Trade(Username.Content, Mon.Content, Id);
                trade.ShowDialog();
            }
            else
            {
                for(int i=0;i<5;i++)
                MessageBox.Show("ONLY FOR ADMIN!!!!!!!!!!!!!!!");
            }
        }

        private void ButtonInventory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowInventory inventory = new WindowInventory(Username.Content.ToString(), Id.ToString(), Mon.Content.ToString());
                inventory.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
