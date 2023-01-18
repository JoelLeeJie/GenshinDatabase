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
        int levelid;
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
                int id = int.Parse(Database.Query(command,0));
                command = "Select c1,c2,c3,c4,c5,c6 from characterconstellations where charid = " + id + ";";
                constellationReader = Database.Query(command);
            }
            catch(Exception error1)
            {
                MessageBox.Show(error1.Message);
                return;
            }
            string temp = "";
            for(int constellation = 1; constellation <7; constellation++)
            {
                temp = temp + $"Constellation {constellation}: {constellationReader[constellation-1].ToString()}\n\n";
            }
            constellationReader.Close();
            CharacterText.Text = temp;
            CharacterText.FontSize = 15;
        }
    }
}
