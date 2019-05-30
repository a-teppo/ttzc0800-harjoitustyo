using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace WpfSalibandyTournament
{
    /// <summary>
    /// Interaction logic for WpfGamestatistics.xaml
    /// </summary>
    public partial class WpfGamestatistics : Window
    {
        //fields
        private WpfGames gamesWindow;
        private List<Goal> homegoals = new List<Goal>();
        private List<Goal> awaygoals = new List<Goal>();
        private List<Penalty> homepenalties = new List<Penalty>();
        private List<Penalty> awaypenalties = new List<Penalty>();
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        private string currentTime = string.Empty;
        private string periodTime = string.Empty;
        private int gameID;
        private bool gameStatus = false;
        //properties
        public List<string> SpecialTeams { get; set; }
        public List<string> NumberOfPeriods { get; set; }
        public List<string> PeriodLenghts { get; set; }
        public List<Player> HomePlayers { get; set; }
        public List<Player> AwayPlayers { get; set; }
        //constructors
        public WpfGamestatistics(WpfGames gamesWindow, Game game)
        {
            InitializeComponent();
            FillCombo(game.KotiId, game.VierasId);
            txtGameId.Text = game.OtteluId.ToString();
            txtHometeam.Text = game.KotiNimi;
            txtHomeId.Text = game.KotiId.ToString();
            txtAwayteam.Text = game.VierasNimi;
            txtAwayId.Text = game.VierasId.ToString();
            CalculateTotalTime(null, null);
            homegoals = DBSalibandytournament.GetGoalsFromDB(game.OtteluId, game.KotiId);
            homepenalties = DBSalibandytournament.GetPenaltiesFromDB(game.OtteluId, game.KotiId);
            awaygoals = DBSalibandytournament.GetGoalsFromDB(game.OtteluId, game.VierasId);
            awaypenalties = DBSalibandytournament.GetPenaltiesFromDB(game.OtteluId, game.VierasId);
            dgHomeGoals.ItemsSource = homegoals;
            dgHomePenalties.ItemsSource = homepenalties;
            dgAwayGoals.ItemsSource = awaygoals;
            dgAwayPenalties.ItemsSource = awaypenalties;
            gameID = game.OtteluId;
            gameStatus = game.Paatetty;
            //disable all controls if game has been ended/saved
            DisableControls();
            this.gamesWindow = gamesWindow;
        }
        //methods
        //Stopwatch
        private void StartTimer(object sender, RoutedEventArgs e)
        {
            //disable changing playtime and enable adding goals and penalties once time is running
            if (lblTimer.Content.ToString() == "00:00")
            {
                btnAddHomeGoal.IsEnabled = true;
                btnAddAwayGoal.IsEnabled = true;
                btnAddHomePenalty.IsEnabled = true;
                btnAddAwayPenalty.IsEnabled = true;
                cmbNumberOfPeriods.IsEnabled = false;
                cmbPeriodLenght.IsEnabled = false;
            }
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += Dt_Tick;
            sw.Start();
            dt.Start();
        }
        void Dt_Tick(object sender, EventArgs e)
        {
            if (sw.IsRunning)
            {
                TimeSpan ts = sw.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
                lblTimer.Content = currentTime;
                foreach (var item in ListTime())
                {
                    //check timelist and stop stopwatch if end of period
                    if (item == currentTime)
                    {
                        sw.Stop();
                        MessageBox.Show("Erä päättyi");
                        //enable end game button only at end of last period
                        if (currentTime == lblTotalTime.Content.ToString())
                        {
                            btnEndGame.IsEnabled = true;
                        }
                    }
                }
            }
        }
        private void StopTimer(object sender, RoutedEventArgs e)
        {
            if (sw.IsRunning)
            {
                sw.Stop();
            }
        }
        private void CalculateTotalTime(object sender, RoutedEventArgs e)
        {
            int periods = int.Parse(cmbNumberOfPeriods.Text);
            double periodlenght = double.Parse(cmbPeriodLenght.Text);
            lblTotalTime.Content = periods * periodlenght + ":00";
        }
        //List times of all period ends, stopwatch stops automatically at the end of each period
        private List<string> ListTime()
        {
            string str = "";
            int periods = int.Parse(cmbNumberOfPeriods.Text);
            double periodlenght = double.Parse(cmbPeriodLenght.Text);
            List<string> timelist = new List<string>();
            for (int i = 1; i <= periods; i++)
            {
                TimeSpan t = TimeSpan.FromMinutes(i * periodlenght);
                str = String.Format("{0:00}:{1:00}", t.TotalMinutes, t.Seconds);
                timelist.Add(str);
            }
            lblTotalTime.Content = str;
            return timelist;
        }
        private void AddGoal(object sender, RoutedEventArgs e)
        {
            try
            {
                Button b = e.Source as Button;
                if (b.Name == "btnAddHomeGoal")
                {
                    int index = homegoals.Count;
                    Goal g = new Goal()
                    {
                        Maalinro = ++index,
                        Aika = currentTime
                    };
                    homegoals.Add(g);
                    dgHomeGoals.ItemsSource = null;
                    dgHomeGoals.ItemsSource = homegoals;
                }
                else if (b.Name == "btnAddAwayGoal")
                {
                    int index = awaygoals.Count;
                    Goal g = new Goal()
                    {
                        Maalinro = ++index,
                        Aika = currentTime
                    };
                    awaygoals.Add(g);
                    dgAwayGoals.ItemsSource = null;
                    dgAwayGoals.ItemsSource = awaygoals;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tapahtui virhe: " + ex.ToString());
            }
        }
        private void AddPenalty(object sender, RoutedEventArgs e)
        {
            try
            {
                Button b = e.Source as Button;
                if (b.Name == "btnAddHomePenalty")
                {
                    int index = homepenalties.Count;
                    Penalty p = new Penalty()
                    {
                        Rangaistusnro = ++index,
                        Aika = currentTime
                    };
                    homepenalties.Add(p);
                    dgHomePenalties.ItemsSource = null;
                    dgHomePenalties.ItemsSource = homepenalties;
                }
                else if (b.Name == "btnAddAwayPenalty")
                {
                    int index = awaypenalties.Count;
                    Penalty p = new Penalty()
                    {
                        Rangaistusnro = ++index,
                        Aika = currentTime
                    };
                    awaypenalties.Add(p);
                    dgAwayPenalties.ItemsSource = null;
                    dgAwayPenalties.ItemsSource = awaypenalties;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tapahtui virhe: " + ex.ToString());
            }
        }
        //fill all combobox sources
        private void FillCombo(int homeID, int awayID)
        {
            NumberOfPeriods = new List<string>() { "1", "2", "3" };
            //0,15 minute period lenght (9 seconds) for testing purposes
            PeriodLenghts = new List<string>() { "0,15", "10", "15", "20" };
            SpecialTeams = new List<string>() { "YV", "AV", "RL", "TM" };
            cmbNumberOfPeriods.ItemsSource = NumberOfPeriods;
            cmbPeriodLenght.ItemsSource = PeriodLenghts;
            cmbErikoistilanneKoti.ItemsSource = SpecialTeams;
            cmbErikoistilanneVieras.ItemsSource = SpecialTeams;
            HomePlayers = DBSalibandytournament.GetTeamPlayersFromDB(homeID);
            AwayPlayers = DBSalibandytournament.GetTeamPlayersFromDB(awayID);
            cmbTekijaKoti.ItemsSource = HomePlayers;
            cmbSyottajaKoti.ItemsSource = HomePlayers;
            cmbKarsijaKoti.ItemsSource = HomePlayers;
            cmbKarsijaVieras.ItemsSource = AwayPlayers;
            cmbTekijaVieras.ItemsSource = AwayPlayers;
            cmbSyottajaVieras.ItemsSource = AwayPlayers;
        }
        private void btnEndGame_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //add goals to database
                string goaltable = "Maali (Aika, Erikoistilanne, Maalintekija, Syottaja, Joukkue, Ottelu)";
                if (homegoals.Count > 0)
                    DBSalibandytournament.InsertIntoDB(goaltable, MakeGoalSql(homegoals, int.Parse(txtHomeId.Text)));
                if (awaygoals.Count > 0)
                    DBSalibandytournament.InsertIntoDB(goaltable, MakeGoalSql(awaygoals, int.Parse(txtAwayId.Text)));
                //add penalties to database
                string penaltytable = "Rangaistus (Aika, Kesto, Syy, Henkilo, Joukkue, Ottelu)";
                if (homepenalties.Count > 0)
                    DBSalibandytournament.InsertIntoDB(penaltytable, MakePenaltySql(homepenalties, int.Parse(txtHomeId.Text)));
                if (awaypenalties.Count > 0)
                    DBSalibandytournament.InsertIntoDB(penaltytable, MakePenaltySql(awaypenalties, int.Parse(txtAwayId.Text)));
                //mark game ended
                DBSalibandytournament.UpdateDB("Ottelu", "Paatetty = TRUE", $"OtteluID = {gameID}");
                gameStatus = true;
                MessageBox.Show("Ottelu on onnistuneesti päätetty.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tapahtui virhe: " + ex.ToString());
            }
        }
        private string MakeGoalSql(List<Goal> goals, int team)
        {
            string retval = "";
            foreach (Goal g in goals)
            {
                string aika = $"'{g.Aika}'";
                string et = $"'{g.Erikoistilanne}'";
                int mt = g.Maalintekija;
                string s = g.Syottaja == null ? "null" : g.Syottaja.ToString();
                int j = team;
                int o = gameID;
                retval += $"({aika},{et},{mt},{s},{j},{o}),";
            }
            //remove comma (,) after last row
            retval = retval.Remove(retval.Length - 1);
            return retval;
        }
        private string MakePenaltySql(List<Penalty> penalties, int team)
        {
            string retval = "";
            foreach (Penalty p in penalties)
            {
                string aika = $"'{p.Aika}'";
                string kesto = $"'{p.Kesto}'";
                string syy = $"'{p.Syy}'";
                int henk = p.Henkilo;
                int j = team;
                int o = gameID;
                retval += $"({aika},{kesto},{syy},{henk},{j},{o}),";
            }
            //remove comma (,) after last row
            retval = retval.Remove(retval.Length - 1);
            return retval;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (gameStatus == false)
            {
                MessageBoxResult result = MessageBox.Show("Ottelua ei ole päätetty (maaleja ja rangaistuksia ei ole tallennettu). Jos suljet ikkunan, tiedot menetetään. Haluatko varmasti sulkea ikkunan?", "Varoitus", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
            gamesWindow.RefreshGames();
        }
        private void DisableControls()
        {
            if (gameStatus == true)
                ccAllControls.IsEnabled = false;
        }

    }
}
