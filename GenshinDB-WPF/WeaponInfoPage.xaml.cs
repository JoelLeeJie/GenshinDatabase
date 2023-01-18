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
    /// Interaction logic for WeaponInfoPage.xaml
    /// </summary>
    public partial class WeaponInfoPage : Page
    {
        List<string> weaponNamesList;
        List<int> levelList = new List<int>() { 1, 20, 40, 50, 60, 70, 80, 90 };
        public WeaponInfoPage()
        {
            
            InitializeComponent();
            weaponNamesList = Database.ListQuery("Select name from weaponinfo", 0);
            WeaponLevelDropdown.ItemsSource = levelList;
            WeaponDropdown.ItemsSource = weaponNamesList;
        }

        private void WeaponDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(WeaponDropdown.SelectedItem.ToString());
        }

        private void WeaponLevelDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
