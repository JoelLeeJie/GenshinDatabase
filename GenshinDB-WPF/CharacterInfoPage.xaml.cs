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
    /// Interaction logic for CharacterInfoPage.xaml
    /// </summary>
    public partial class CharacterInfoPage : Page
    {
        List<string> characterNamesList;
        List<int> levelList = new List<int>() { 1, 20, 40, 50, 60, 70, 80, 90 };
        int levelid = 0;
        string characterName = "";
        public CharacterInfoPage()
        {
            InitializeComponent();
            characterNamesList = Database.ListQuery("Select name from characterinfo", 0);
            CharacterDropdown.ItemsSource = characterNamesList;
            CharacterLevelDropdown.ItemsSource = levelList;
        }

        private void CharacterDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            characterName = CharacterDropdown.SelectedItem.ToString();
        }


        private void CharacterLevelDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            levelid = int.Parse(CharacterLevelDropdown.SelectedItem.ToString());
        }

        private void CharacterConstellationButton_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlDataReader idReader;
            NpgsqlDataReader constellationReader;
            if (characterName == "")
            {
                CharacterText.Text = "Select a Character";
                return;
            }
            try
            {
                string command = "Select id from characterinfo where name = '" + characterName + "';";
                int id = int.Parse(Database.Query(command, 0));
                command = "Select c1,c2,c3,c4,c5,c6 from characterconstellations where charid = " + id + ";";
                constellationReader = Database.Query(command);
            }
            catch (Exception error1)
            {
                MessageBox.Show(error1.Message);
                return;
            }
            string temp = $"Name: {characterName}\n\n";
            for (int constellation = 1; constellation < 7; constellation++)
            {
                temp += $"Constellation {constellation}: {constellationReader[constellation - 1].ToString()}\n\n";
            }
            constellationReader.Close();

            CharacterText.Text = temp;
            CharacterText.FontSize = 15;
        }

        private void CharacterInfoButton_Click(object sender, RoutedEventArgs e)
        {
            CharacterText.FontSize = 13;
            NpgsqlDataReader reader;
            if (characterName == "")
            {
                CharacterText.Text = "Select a Character";
                return;
            }
            try
            {
                string command = "Select * from characterinfo where name = '" + characterName + "';";
                reader = Database.Query(command);
            }
            catch (Exception error2)
            {
                MessageBox.Show(error2.Message);
                return;
            }

            string temp = "";
            try
            {
                temp += "Name: " + characterName + "\n";
                temp += $"Rarity: {reader[6].ToString()}*\n";
                temp += $"Region: {reader[5].ToString()}\n";
                temp += $"Weapon: {reader[4].ToString()}\n";
                temp += $"Vision: {reader[3].ToString()}\n\n";
                temp += $"Background: {reader[2].ToString()}\n\n";
                temp += $"{reader[7].ToString()}\n\n"; //Normal attacks.
                temp += $"Elemental Skill: {reader[8].ToString()}\n\n";
                temp += $"Elemental Burst: {reader[9].ToString()}";
                temp = temp.Replace("Normal Attack", "Normal Attack: ").Replace(" Charged Attack", "\nCharged Attack: ").Replace(" Plunging Attack", "\nPlunging Attack: ");
            }
            catch (Exception error1)
            {
                MessageBox.Show("Error getting info from reader. " + error1.Message);
                return;
            }
            finally
            {
                reader.Close();
            }

            CharacterText.Text = temp;
        }

        private void CharacterStatsButton_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlDataReader reader;
            CharacterText.FontSize = 18;
            CharacterText.Text = "";
            if (characterName == "") CharacterText.Text += "Select a Character";
            if (levelid == 0) CharacterText.Text += "Select a Level";
            if (CharacterText.Text != "") return; //Check if both character and level is entered.

            string command = $"Select id from characterinfo where name = '{characterName}';";
            int charid;
            if (!int.TryParse(Database.Query(command, 0), out charid)) //get charid, to use with levelid to get row.
            {
                return;
            }
            string temp = "";
            try
            {
                command = $"SELECT * FROM characterstats WHERE charid = {charid} AND levelid = {levelid};";
                reader = Database.Query(command);

                temp += $"Name: {characterName}\n";
                if (levelid == 1) temp += "Level: 1/20\n";
                else temp += $"Level: {levelid}/{levelid}\n\n";

                temp += $"HP| {reader[2].ToString()}\n";
                temp += $"ATK| {reader[3].ToString()}\n";
                temp += $"DEF| {reader[4].ToString()}\n";
                temp += reader[5].ToString().ToUpper() + "| " + reader[6].ToString(); //acensionstatname| ascensionstatvalue.
                reader.Close();
            }

            catch (Exception error1)
            {
                MessageBox.Show(error1.Message);
            }

            CharacterText.Text = temp;
        }

    }
}

