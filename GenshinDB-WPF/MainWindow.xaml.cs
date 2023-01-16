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
using System.Data;
using System.Data.SqlClient;
using Npgsql;
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
        public NpgsqlConnection GetConnection()
        {
            string connectionstring = @"Server=containers-us-west-53.railway.app;Port=7418;Database=railway;User Id=dbuser;Password=genshin123;";
            return new NpgsqlConnection(connectionstring);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ResultBox.Text = "";

            using (NpgsqlConnection dbconnection = GetConnection())
            {
                dbconnection.Open(); //opens sql connection
                using (NpgsqlCommand command1 = new NpgsqlCommand("Select * from artifactstats", dbconnection))
                {//creates command
                    using (NpgsqlDataReader reader = command1.ExecuteReader())
                    {//runs command, and returns reader object.
                        while(reader.Read()) //while there's still more rows, shift reader to next row.
                        {
                            ResultBox.Text = ResultBox.Text + reader[0].ToString();//uses the reader object to read the specified column. Does not read the whole row.
                            ResultBox.Text = ResultBox.Text + reader[1].ToString() + "\n";
                        }
                        Console.WriteLine(ResultBox.Text);
                    }
                }
                    
            }
            
        }

    }
}
