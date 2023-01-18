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

namespace GenshinDB_WPF
{
    /// <summary>
    /// Interaction logic for ArtifactInfoPage.xaml
    /// </summary>
    public partial class ArtifactInfoPage : Page
    {
        public List<string> artifactNamesList;
        public ArtifactInfoPage()
        {
            InitializeComponent();
            artifactNamesList = Database.ListQuery("Select name from artifactsets;", 0);
            ArtifactDropdown.ItemsSource = artifactNamesList;
        }

        private void ArtifactDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(ArtifactDropdown.SelectedItem.ToString());
        }

    }
}
