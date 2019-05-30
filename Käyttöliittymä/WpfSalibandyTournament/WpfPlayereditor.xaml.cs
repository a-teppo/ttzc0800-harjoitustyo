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
            try
            {
                //check that all info is correctly filled in
                if (FillOK() != "ok")
                {
                    MessageBox.Show(FillOK());
                    return;
                }
                string Lastname = $"'{txtLastname.Text}'";
                string Firstname = $"'{txtFirstname.Text}'";
                string Playernumber = $"'{txtPlayernumber.Text}'";
                string Position = $"'{txtPosition.Text}'";
                string Birthyear = txtBirthday.Text == "" ? "NULL" : txtBirthday.Text;
                string Role = $"'{txtRole.Text}'";
                bool parseok = Int32.TryParse(cmbTeams.SelectedValue.ToString(), out int TeamID);
                //if saving new person
                if (string.IsNullOrWhiteSpace(txtID.Text))
                {
                    string inserttablestring = "Henkilot (Sukunimi, Etunimi, Pelinumero, Pelipaikka, Syntymavuosi, Rooli, JoukkueID)";
                    string insertvaluestring = $"({Lastname}, {Firstname}, {Playernumber}, {Position}, {Birthyear}, {Role}, {TeamID})";
                    DBSalibandytournament.InsertIntoDB(inserttablestring, insertvaluestring);
                    playerWindow.RefreshPlayers();
                }
                //if updating existing person
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
            catch (Exception ex)
            {
                MessageBox.Show("Tapahtui virhe: " + ex.ToString());
            }
        }
        private string FillOK()
        {

            if (txtLastname.Text == "" || txtFirstname.Text == "" || txtPlayernumber.Text == "" || cmbTeams.SelectedValue == null)
                return "Täytä kaikki tähdellä merkityt kohdat ennen tallentamista.";
            bool parseok = Int32.TryParse(cmbTeams.SelectedValue.ToString(), out int TeamID);
            List<Player> players = DBSalibandytournament.GetTeamPlayersFromDB(TeamID);
            foreach (Player p in players)
            {
                if (txtPlayernumber.Text == p.Pelinumero && txtID.Text != p.HenkiloId.ToString())
                    return $"Pelinumero on jo käytössä joukkueessa {cmbTeams.Text}.";
            }
            return "ok";
        }
    }
}
