using System;
using System.Collections.Generic;
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
        private List<Goal> goals = new List<Goal>();
        private int homegoalnumber = 0;
        private int homepenaltynumber = 0;
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        private string currentTime = string.Empty;
        public WpfGamestatistics()
        {
            InitializeComponent();
        }
        public class Goal
        {
            public int Maalinro { get; set; }
            public string Aika { get; set; }
            public string Erikoistilanne { get; set; }
            public int Maalintekija { get; set; }
            public int Syottaja { get; set; }
            public int Ottelu { get; set; }
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
            }
        }
        private void StopTimer(object sender, RoutedEventArgs e)
        {
            if (sw.IsRunning)
            {
                sw.Stop();
            }
        }
        private void AddHomeGoal(object sender, RoutedEventArgs e)
        {
            homegoalnumber += 1;
            var data = new Goal { Maalinro = homegoalnumber, Aika = currentTime };
            dgHomeGoals.Items.Add(data);
        }

        private void AddHomePenalty(object sender, RoutedEventArgs e)
        {
            homepenaltynumber += 1;
            var data = new Penalty { Rangaistusnro = homepenaltynumber };
            dgHomePenalties.Items.Add(data);
        }
        
    }
}
