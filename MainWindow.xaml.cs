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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace EuroGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection conn = new("Server=127.0.0.1;Database=eurovizio;Uid=root;Pwd=;CharSet=utf8");
        ObservableCollection<Dal> lista = new();

        public MainWindow()
        {
            InitializeComponent();

            Feltolt();
            dgDagiDaganat.ItemsSource = lista;
            dgDagiDaganat.SelectedIndex = 0;
        }

        private void Feltolt()
        {
            MySqlCommand cmd = new("SELECT * FROM dal;", conn);
            conn.Open();

            using(MySqlDataReader reader = cmd.ExecuteReader())
            {
                while(reader.Read())
                    lista.Add(
                        new(
                            reader.GetInt32("ev"),
                            reader.GetString("eloado"),
                            reader.GetString("cim"),
                            reader.GetInt32("helyezes"),
                            reader.GetInt32("pontszam")

                        ));
            }

            conn.Close();
        }
    }

    public record Dal(int Ev, string Eloado, string Cim, int Helyezes, int Pontszam);
}
