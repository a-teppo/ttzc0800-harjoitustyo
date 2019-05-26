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
        private List<Goal> homegoals = new List<Goal>();
        private List<Goal> awaygoals = new List<Goal>();
        private int homegoalnumber = 0;
        private List<Penalty> homepenalties = new List<Penalty>();
        private int homepenaltynumber = 0;
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        private string currentTime = string.Empty;
        private string periodTime = string.Empty;
        public List<string> Erikoistilanteet { get; set; }
        
        public WpfGamestatistics(Game game)
        {
            InitializeComponent();
            FillCombo();
            txtGameID.Text = game.OtteluId.ToString();
            txtHometeam.Text = game.KotiNimi;
            homegoals = GetGoalsFromDB(game.OtteluId, game.KotiId);
            //awaygoals = GetGoalsFromDB(game.OtteluId, game.VierasId);
            dgHomeGoals.ItemsSource = homegoals;
            //dgAwayGoals.ItemsSource = awaygoals;
        }
        public class Goal
        {
            public int MaaliID { get; set; }
            public int Maalinro { get; set; }
            public string Aika { get; set; }
            public string Erikoistilanne { get; set; }
            public string Maalintekija { get; set; }
            public string Syottaja { get; set; }
            public int Ottelu { get; set; }
            public int Joukkue { get; set; }
        }
        public class Penalty
        {
            public int Rangaistusnro { get; set; }
            public string Aika { get; set; }
            public int Kesto { get; set; }
            public string Syy { get; set; }
            public int Henkilo { get; set; }
            public int Ottelu { get; set; }
        }
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
                foreach(var item in ListTime())
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
        private List<string> ListTime()
        {
            string str = "";
            int periods = int.Parse(txtNumberOfPeriods.Text);
            double periodlenght = double.Parse(txtPeriodLenght.Text);
            List<string> timelist = new List<string>();
            for(int i = 1; i <= periods; i++)
            { 
                TimeSpan t = TimeSpan.FromMinutes(i * periodlenght);
                str = String.Format("{0:00}:{1:00}", t.Minutes, t.Seconds);
                timelist.Add(str);
            }
            lblHelper.Content = str;
            return timelist;
        }
        private void AddGoal(object sender, RoutedEventArgs e)
        {
            Button b = e.Source as Button;
            if (b.Name == "AddHomeGoal")
            {
                homegoalnumber += 1;
                Goal g = new Goal()
                {
                    Maalinro = homegoalnumber,
                    Aika = currentTime
                };
                homegoals.Add(g);
                dgHomeGoals.ItemsSource = null;
                dgHomeGoals.ItemsSource = homegoals;
            }
            /*else if (b.Name == "AddAwayGoal")
            {
                awaygoalnumber += 1;
                Goal g = new Goal()
                {
                    Maalinro = awaygoalnumber,
                    Aika = currentTime
                };
                awaygoals.Add(g);
                dgAwayGoals.ItemsSource = null;
                dgAwayGoals.ItemsSource = awaygoals;
            }*/
        }
        private void AddPenalty(object sender, RoutedEventArgs e)
        {
            Button b = e.Source as Button;
            if (b.Name == "AddHomePenalty")
            {
                homepenaltynumber += 1;
                Penalty p = new Penalty()
                {
                    Rangaistusnro = homepenaltynumber,
                    Aika = currentTime
                };
                homepenalties.Add(p);
                dgHomePenalties.ItemsSource = null;
                dgHomePenalties.ItemsSource = homepenalties;
            }
        }
        private void FillCombo()
        {
            Erikoistilanteet = new List<string>() { "YV", "AV", "RL" };
            cmbErikoistilanne.ItemsSource = Erikoistilanteet;
        }
        public static List<Goal> GetGoalsFromDB(int gameID, int teamID)
        {
            List<Goal> goals = new List<Goal>();
            string str = $"Maali WHERE Ottelu = {gameID} AND Joukkue = {teamID} ORDER BY Aika";
            DataTable dt = DBSalibandytournament.GetViewDB(str);
            int maalinro = 0;
            foreach (DataRow item in dt.Rows)
            {
                Goal g = new Goal()
                {
                    Maalinro = ++maalinro,
                    MaaliID = int.Parse(item[0].ToString()),
                    Aika = item[1].ToString(),
                    Erikoistilanne = item[2].ToString(),
                    Maalintekija = item[3].ToString(),
                    Syottaja = item[4].ToString(),
                    Joukkue = int.Parse(item[5].ToString()),
                    Ottelu = int.Parse(item[6].ToString())
                };
                goals.Add(g);
            }
            return goals;
        }
    }
}
