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
        private WpfPlayers playerWindow;
        public List<Team> Teams { get; set; }
        Player player;
        public WpfPlayereditor(WpfPlayers playerWindow)
        {
            InitializeComponent();
            Teams = DBSalibandytournament.GetTeamsFromDB();
            cmbTeams.ItemsSource = Teams;
            this.playerWindow = playerWindow;
        }
        public WpfPlayereditor(WpfPlayers playerWindow, Player player)
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
            cmbTeams.SelectedValue = player.JoukkueId;
            Teams = DBSalibandytournament.GetTeamsFromDB();
            cmbTeams.ItemsSource = Teams;
            this.playerWindow = playerWindow;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {            
            Close();
            playerWindow.RefreshPlayers();
        }

        private void btnSavePerson_Click(object sender, RoutedEventArgs e)
        {
            string Lastname = $"'{txtLastname.Text}'";
            string Firstname = $"'{txtFirstname.Text}'";
            string Playernumber = $"'{txtPlayernumber.Text}'";
            string Position = $"'{txtPosition.Text}'";
            int Birthyear = int.Parse(txtBirthday.Text);
            string Role = $"'{txtRole.Text}'";
            bool parseok = Int32.TryParse(cmbTeams.SelectedValue.ToString(), out int TeamID);             
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                string inserttablestring = "Henkilot (Sukunimi, Etunimi, Pelinumero, Pelipaikka, Syntymavuosi, Rooli, JoukkueID)";
                string insertvaluestring = $"({Lastname}, {Firstname}, {Playernumber}, {Position}, {Birthyear}, {Role}, {TeamID})";
                DBSalibandytournament.InsertIntoDB(inserttablestring, insertvaluestring);
                playerWindow.RefreshPlayers();
            }
            else
            {
                int PersonID = int.Parse(txtID.Text);
                string updatetablestring = "Henkilot";
                string updatevaluestring = $"Sukunimi = {Lastname}, Etunimi = {Firstname}, Pelinumero = {Playernumber}, Pelipaikka = {Position}, Syntymavuosi = {Birthyear}, Rooli = {Role}, JoukkueID = {TeamID}";
                string updatewherestring = $"HenkiloID = {PersonID}";
                DBSalibandytournament.UpdateDB(updatetablestring, updatevaluestring, updatewherestring);
                playerWindow.RefreshPlayers();
            }
            Close();
            playerWindow.RefreshPlayers();
        }      
    }
}
