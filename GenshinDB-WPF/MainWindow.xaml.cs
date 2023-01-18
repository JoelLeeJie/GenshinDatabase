using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace GenshinDB_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Character_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new CharacterInfoPage();
        }

        private void Artifact_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new ArtifactInfoPage();
        }

        private void Weapon_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new WeaponInfoPage();
        }

        private void Config_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new ConfigPage();
        }
    }
}
