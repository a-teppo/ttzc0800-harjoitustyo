﻿using System;
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
    /// Interaction logic for WpfGameEditor.xaml
    /// </summary>
    public partial class WpfGameEditor : Window
    {
        //fields
        private WpfGames gamesWindow;
        private Game game;
        //properties
        public List<Team> Teams { get; set; }
        //constructors
        public WpfGameEditor(WpfGames gamesWindow)
        {
            InitializeComponent();
            FillCombo();
            this.gamesWindow = gamesWindow;
            //if new game, default start time is now
            txtTime.Text = DateTime.Now.ToString();
        }
        public WpfGameEditor(WpfGames gamesWindow, Game game)
        {
            InitializeComponent();
            this.game = game;
            txtID.Text = game.OtteluId.ToString();
            txtLocation.Text = game.Paikka;
            txtTime.Text = game.Aika;
            cmbHomeTeam.SelectedValue = game.KotiId;
            cmbAwayTeam.SelectedValue = game.VierasId;
            FillCombo();
            this.gamesWindow = gamesWindow;
        }
        //methods
        private void FillCombo()
        {
            Teams = DBSalibandytournament.GetTeamsFromDB();
            cmbHomeTeam.ItemsSource = Teams;
            cmbAwayTeam.ItemsSource = Teams;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FillOK() != "ok")
                {
                    MessageBox.Show(FillOK());
                    return;
                }
                //convert time textbox to the right format for SQL
                DateTime date = Convert.ToDateTime(txtTime.Text);
                string Time = $"'{date.ToString("yyyy-MM-dd HH:mm:ss")}'";
                string Location = $"'{txtLocation.Text}'";
                bool parseokHome = Int32.TryParse(cmbHomeTeam.SelectedValue.ToString(), out int HomeTeamID);
                bool parseokAway = Int32.TryParse(cmbAwayTeam.SelectedValue.ToString(), out int AwayTeamID);
                //if saving new game
                if (string.IsNullOrWhiteSpace(txtID.Text))
                {
                    string inserttablestring = "Ottelu (Aika, Paikka, Kotijoukkue, Vierasjoukkue, Paatetty)";
                    string insertvaluestring = $"({Time}, {Location}, {HomeTeamID}, {AwayTeamID}, FALSE)";
                    DBSalibandytournament.InsertIntoDB(inserttablestring, insertvaluestring);
                }
                //if updating existing game
                else
                {
                    int GameID = int.Parse(txtID.Text);
                    string updatetablestring = "Ottelu";
                    string updatevaluestring = $"Aika = {Time}, Paikka = {Location}, Kotijoukkue = {HomeTeamID}, Vierasjoukkue = {AwayTeamID}";
                    string updatewherestring = $"OtteluID = {GameID}";
                    DBSalibandytournament.UpdateDB(updatetablestring, updatevaluestring, updatewherestring);
                }
                Close();
                gamesWindow.RefreshGames();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tapahtui virhe: " + ex.ToString());
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private string FillOK()
        {
            if (cmbAwayTeam.Text == cmbHomeTeam.Text)
                return "Koti- ja vierasjoukkue ei voi olla sama.";
            //check that all required info is filled in
            if (txtTime.Text == "" || txtLocation.Text == "" || cmbHomeTeam.SelectedValue == null || cmbAwayTeam.SelectedValue == null)
                return "Täytä kaikki tähdellä merkityt kentät ennen tallentamista.";
            else
                return "ok";
        }
    }
}
