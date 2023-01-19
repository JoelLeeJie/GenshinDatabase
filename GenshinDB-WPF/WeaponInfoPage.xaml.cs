using System;
using System.Collections.Generic;
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
using Npgsql;

namespace GenshinDB_WPF
{
    /// <summary>
    /// Interaction logic for WeaponInfoPage.xaml
    /// </summary>
    public partial class WeaponInfoPage : Page
    {
        List<string> weaponNamesList;
        List<int> levelList = new List<int>() { 1, 20, 40, 50, 60, 70, 80, 90 };
        string weaponName = "";
        int levelid = 0;
        int weaponid = 0;
        NpgsqlDataReader reader;
        public WeaponInfoPage()
        {
            
            InitializeComponent();
            weaponNamesList = Database.ListQuery("Select name from weaponinfo", 0);
            WeaponLevelDropdown.ItemsSource = levelList;
            WeaponDropdown.ItemsSource = weaponNamesList;
        }

        private void WeaponDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            weaponName = WeaponDropdown.SelectedItem.ToString();
            weaponid = WeaponDropdown.SelectedIndex + 1; //*This is a workaround. Since can't get weaponid via database search(some names like Amos' Bow can mess up the search, get it from the index instead.

        }

        private void WeaponLevelDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            levelid = int.Parse(WeaponLevelDropdown.SelectedItem.ToString());
        }

        private void WeaponRefinementButton_Click(object sender, RoutedEventArgs e)
        {
            WeaponText.FontSize = 13;
            if (weaponName == "")
            {
                WeaponText.Text = "Select a Weapon";
                return;
            }

            string command = $"Select * from weaponrefinements where weaponid = {weaponid};";
            string temp = "";
            try
            {
                reader = Database.Query(command);
                temp += $"Name: {weaponName}\n\n";
                for(int refinement = 1; refinement<6; refinement++)
                {
                    temp += $"Refinement {refinement}: {reader[refinement].ToString()}\n\n";
                }
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
            WeaponText.Text=temp;
            reader.Close();
        }

        private void WeaponInfoButton_Click(object sender, RoutedEventArgs e)
        {
            WeaponText.FontSize = 18;
            if(weaponName == "")
            {
                WeaponText.Text = "Select a Weapon";
                return;
            }
            string temp = "";
            string command = $"Select * from weaponinfo where id = {weaponid}";
            try
            {
                reader = Database.Query(command);
                temp += $"Name: {weaponName}\n";
                temp += $"Rarity: {reader[3].ToString()}\n";
                temp += $"Type: {reader[2].ToString()}";
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }

            reader.Close();
            WeaponText.Text = temp;
        }

        private void WeaponStatsButton_Click(object sender, RoutedEventArgs e)
        {
            WeaponText.FontSize = 20;
            WeaponText.Text = "";
            if (weaponName == "") WeaponText.Text += "Select a Weapon\n";
            if (levelid == 0) WeaponText.Text += "Select a Level";
            if (WeaponText.Text != "") return;
            string temp = "";
            string command = $"Select * from weaponstats where weaponid = {weaponid} and levelid = {levelid};";
            try
            {
                reader = Database.Query(command);
                temp += $"Name: {weaponName}\n";
                if (levelid == 1) temp += "Level: 1/20\n";
                else temp += $"Level: {levelid}/{levelid}\n";
                temp += $"Base ATK| {reader[2].ToString()}\n";
                temp += $"{reader[3].ToString().ToUpper()}| {reader[4].ToString()}";
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
            reader.Close();
            WeaponText.Text = temp;
        }
    }
}
