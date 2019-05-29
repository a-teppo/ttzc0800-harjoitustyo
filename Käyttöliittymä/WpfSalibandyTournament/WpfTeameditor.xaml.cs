using Microsoft.Win32; //lisätty
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
using System.Windows.Navigation; //lisätty
using System.Windows.Shapes;

namespace WpfSalibandyTournament
{
    /// <summary>
    /// Interaction logic for WpfTeameditor.xaml
    /// </summary>
    public partial class WpfTeameditor : Window
    {
        private WpfTeams teamsWindow;
        private Team team;

        public List<Team> Teams { get; set; }

        public WpfTeameditor(WpfTeams teamsWindow)
        {
            InitializeComponent();
            Teams = DBSalibandytournament.GetTeamsFromDB();
            this.teamsWindow = teamsWindow;
        }

        public WpfTeameditor(WpfTeams teamsWindow, Team team)
        {
            InitializeComponent();
            this.team = team;
            txtTeamID.Text = team.JoukkueId.ToString();
            txtTeamname.Text = team.Nimi;
            txtLocation.Text = team.Paikkakunta;
            txtOrganization.Text = team.Seura;
            txtLogofile.Text = team.LogoURL;
            this.teamsWindow = teamsWindow;
        }

        private void btnLogofile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
              txtLogofile.Text = dlg.FileName;
        }

        private void btnSaveTeam_Click(object sender, RoutedEventArgs e)
        {
            
            string Nimi = $"'{txtTeamname.Text }'";
            string Paikkakunta = $"'{txtLocation.Text}'";
            string Seura = $"'{txtOrganization.Text}'";
            string LogoURL = $"'{txtLogofile.Text}'";

            if (string.IsNullOrWhiteSpace(txtTeamID.Text))
            {
                string inserttablestring = "Joukkue (Nimi, Paikkakunta, Seura, LogoURL)";
                string insertvaluestring = $"({Nimi}, {Paikkakunta}, {Seura}, {LogoURL})";
                DBSalibandytournament.InsertIntoDB(inserttablestring, insertvaluestring);
            }
            else
            {
                int JoukkueId = int.Parse(txtTeamID.Text);
                string updatetablestring = "Joukkue";
                string updatevaluestring = $"Nimi = {Nimi}, Paikkakunta = {Paikkakunta}, Seura = {Seura}, LogoURL = {LogoURL}";
                string updatewherestring = $"JoukkueId = {JoukkueId}";
                DBSalibandytournament.UpdateDB(updatetablestring, updatevaluestring, updatewherestring);

            }
            Close();
            teamsWindow.RefreshTeams();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
            teamsWindow.RefreshTeams();
        }

        private void OpenTeam()
        {
            WpfTeams teams = new WpfTeams();
            teams.Show();
        }
    }
}
