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
using System.Windows.Shapes;

namespace WpfSalibandyTournament
{
    /// <summary>
    /// Interaction logic for WpfPlayereditor.xaml
    /// </summary>
    public partial class WpfPlayereditor : Window
    {
        public WpfPlayereditor()
        {
            InitializeComponent();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSavePerson_Click(object sender, RoutedEventArgs e)
        {
            string Lastname = $"'{txtLastname.Text}'";
            string Firstname = $"'{txtFirstname.Text}'";
            int Playernumber = int.Parse(txtPlayernumber.Text);
            string Position = $"'{txtPosition.Text}'";
            int Birthyear = int.Parse(txtBirthday.Text);
            string Role = $"'{txtRole.Text}'";
            int TeamID = 1;
            string tablestring = "Henkilot (Sukunimi, Etunimi, Pelinumero, Pelipaikka, Syntymavuosi, Rooli, JoukkueID)";
            string valuestring = $"({Lastname}, {Firstname}, {Playernumber}, {Position}, {Birthyear}, {Role}, {TeamID})";
            DBSalibandytournament.InsertIntoDB(tablestring, valuestring);
            Close();
        }
    }
}
