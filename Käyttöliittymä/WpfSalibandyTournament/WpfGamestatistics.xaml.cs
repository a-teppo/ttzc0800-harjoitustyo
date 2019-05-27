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
        private List<Goal> homegoals = new List<Goal>();
        private List<Goal> awaygoals = new List<Goal>();
        private List<Penalty> homepenalties = new List<Penalty>();
        private List<Penalty> awaypenalties = new List<Penalty>();
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        private string currentTime = string.Empty;
        private string periodTime = string.Empty;
        //properties (used in comboboxes)
        public List<string> Erikoistilanteet { get; set; }
        public List<Player> HomePlayers { get; set; }
        public List<Player> AwayPlayers { get; set; }
        //constructors
        public WpfGamestatistics(Game game)
        {
            InitializeComponent();
            FillCombo(game.KotiId, game.VierasId);
            txtGameID.Text = game.OtteluId.ToString();
            txtHometeam.Text = game.KotiNimi;
            CalculateTotalTime(null, null);
            homegoals = DBSalibandytournament.GetGoalsFromDB(game.OtteluId, game.KotiId);
            homepenalties = DBSalibandytournament.GetPenaltiesFromDB(game.OtteluId, game.KotiId);
            awaygoals = DBSalibandytournament.GetGoalsFromDB(game.OtteluId, game.VierasId);
            awaypenalties = DBSalibandytournament.GetPenaltiesFromDB(game.OtteluId, game.VierasId);
            dgHomeGoals.ItemsSource = homegoals;
            dgHomePenalties.ItemsSource = homepenalties;
            dgAwayGoals.ItemsSource = awaygoals;
            dgAwayPenalties.ItemsSource = awaypenalties;
        }
        //methods
        //Timer
        private void StartTimer(object sender, RoutedEventArgs e)
        {
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
                    if (item == currentTime)
                        sw.Stop();
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
            int periods = int.Parse(txtNumberOfPeriods.Text);
            int periodlenght = int.Parse(txtPeriodLenght.Text);
            lblTotalTime.Content = periods*periodlenght+":00";
        }
        //List times of all period ends
        private List<string> ListTime()
        {
            string str = "";
            int periods = int.Parse(txtNumberOfPeriods.Text);
            double periodlenght = double.Parse(txtPeriodLenght.Text);
            List<string> timelist = new List<string>();
            for (int i = 1; i <= periods; i++)
            {
                TimeSpan t = TimeSpan.FromMinutes(i * periodlenght);
                str = String.Format("{0:00}:{1:00}", t.Minutes, t.Seconds);
                timelist.Add(str);
            }
            lblTotalTime.Content = str;
            return timelist;
        }
        private void AddGoal(object sender, RoutedEventArgs e)
        {
            Button b = e.Source as Button;
            if (b.Name == "AddHomeGoal")
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
            else if (b.Name == "AddAwayGoal")
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
        private void AddPenalty(object sender, RoutedEventArgs e)
        {
            Button b = e.Source as Button;
            if (b.Name == "AddHomePenalty")
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
            else if (b.Name == "AddHomePenalty")
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
        private void FillCombo(int homeID, int awayID)
        {
            Erikoistilanteet = new List<string>() { "YV", "AV", "RL" };
            cmbErikoistilanneKoti.ItemsSource = Erikoistilanteet;
            cmbErikoistilanneVieras.ItemsSource = Erikoistilanteet;
            HomePlayers = DBSalibandytournament.GetTeamPlayersFromDB(homeID);
            AwayPlayers = DBSalibandytournament.GetTeamPlayersFromDB(awayID);
            cmbTekijaKoti.ItemsSource = HomePlayers;
            cmbSyottajaKoti.ItemsSource = HomePlayers;
            cmbKarsijaKoti.ItemsSource = HomePlayers;
            cmbKarsijaVieras.ItemsSource = AwayPlayers;
            cmbTekijaVieras.ItemsSource = AwayPlayers;
            cmbSyottajaVieras.ItemsSource = AwayPlayers;
        }
        //get current indexnumber for goals and penalties when adding new rows
        private int GetIndex<T>(List<T> data)
        {
            int i = 1;
            foreach (var foo in data)
            {
                i++;
            }
            return i;
        }
    }
}
