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
    /// Interaction logic for ArtifactInfoPage.xaml
    /// </summary>
    public partial class ArtifactInfoPage : Page
    {
        public List<string> artifactNamesList;
        public List<string> artifactTypesList;
        string name = "";
        string type = "";
        int rarity = 0;
        NpgsqlDataReader reader;
        public ArtifactInfoPage()
        {
            InitializeComponent();
            artifactNamesList = Database.ListQuery("Select name from artifactsets;", 0);
            artifactTypesList = Database.ListQuery("Select distinct type from artifactstats;", 0);
            ArtifactDropdown.ItemsSource = artifactNamesList;
            ArtifactStatsDropdown.ItemsSource = artifactTypesList;
            ArtifactRarityDropdown.ItemsSource = new List<string>() { "3", "4", "5" };
        }

        private void ArtifactDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            name = ArtifactDropdown.SelectedItem.ToString();
        }

        private void ArtifactStatsDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            type = ArtifactStatsDropdown.SelectedItem.ToString();
        }

        private void ArtifactRarityDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rarity = int.Parse(ArtifactRarityDropdown.SelectedItem.ToString());
        }

        private void ArtifactInfoButton_Click(object sender, RoutedEventArgs e)
        {
            if (name == "")
            {
                ArtifactText.Text = "Select an Artifact Set";
                return;
            }
            string temp = "";
            string command = $"Select * from artifactsets where id = {ArtifactDropdown.SelectedIndex+1};";
            try
            {
                reader = Database.Query(command);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message + "\n Invalid query");
            }
            try
            {
                temp = $"Name: {ArtifactDropdown.SelectedItem.ToString()}\n";
                temp += $"1-piece: {reader[4].ToString()}\n";
                temp += $"2-piece: {reader[2].ToString()}\n";
                temp += $"4-piece: {reader[3].ToString()}";
            }
            catch
            {
                MessageBox.Show("Unable to get data");
            }
            ArtifactText.Text = temp;
            reader.Close();
        }

        private void ArtifactStatsButton_Click(object sender, RoutedEventArgs e)
        {
            string temp = "";
            if (type == "") temp = "Select an Artifact Type\n";
            if (rarity == 0) temp += "Select rarity";
            if (temp != "")
            {
                ArtifactText.Text = temp;
                return;
            }
            temp = "";
            int level = 0;
            string command = $"Select statname, statvalue from artifactstats where type = '{type}' and rarity = '{rarity}' order by id;";

            try
            {
                reader = Database.Query(command);
                temp = $"Type = {type}\n";
                temp += $"Rarity = {rarity}\n";
                switch(rarity)
                {
                    case 3: level = 12; break;
                    case 4: level = 16; break;
                    case 5: level = 20; break;
                }
                temp += $"Level: {level}\n\n";
                do
                {
                    temp += $"{reader[0].ToString()}| {reader[1].ToString()}\n";
                }
                while(reader.Read());
                ArtifactText.Text = temp;
            }
            catch (Exception error1)
            {
                MessageBox.Show(error1.Message + "\n Invalid Query");
            }
            finally
            {
                reader.Close();
            }
        }
    }
}
