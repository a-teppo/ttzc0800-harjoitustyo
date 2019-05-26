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
        Player player;
        public WpfPlayereditor()
        {
            InitializeComponent();
        }
        public WpfPlayereditor(Player player)
        {
            InitializeComponent();
            this.player = player;
            txtID.Text = player.HenkiloId.ToString();
            txtLastname.Text = player.Sukunimi;
            txtFirstname.Text = player.Etunimi;
            txtBirthday.Text = player.Syntymavuosi.ToString();
            txtPlayernumber.Text = player.Pelinumero.ToString();
            txtPosition.Text = player.Pelipaikka;
            txtRole.Text = player.Rooli;
            txtTeams.Text = player.JoukkueId.ToString();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            OpenPlayers();
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
            int TeamID = 2;
            
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                string inserttablestring = "Henkilot (Sukunimi, Etunimi, Pelinumero, Pelipaikka, Syntymavuosi, Rooli, JoukkueID)";
                string insertvaluestring = $"({Lastname}, {Firstname}, {Playernumber}, {Position}, {Birthyear}, {Role}, {TeamID})";
                DBSalibandytournament.InsertIntoDB(inserttablestring, insertvaluestring);
            }
            else
            {
                int PersonID = int.Parse(txtID.Text);
                string updatetablestring = "Henkilot";
                string updatevaluestring = $"Sukunimi = {Lastname}, Etunimi = {Firstname}, Pelinumero = {Playernumber}, Pelipaikka = {Position}, Syntymavuosi = {Birthyear}, Rooli = {Role}, JoukkueID = {TeamID}";
                string updatewherestring = $"HenkiloID = {PersonID}";
                DBSalibandytournament.UpdateDB(updatetablestring, updatevaluestring, updatewherestring);
                OpenPlayers();
            }
            Close();
        }
        private void OpenPlayers()
        {
            WpfPlayers players = new WpfPlayers();
            players.Show();
        }
    }
}
