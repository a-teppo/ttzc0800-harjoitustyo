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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;


namespace WpfSalibandyTournament
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {       
        public MainWindow()
        {
            InitializeComponent();                        
        }        
        private void BtnGames_Click(object sender, RoutedEventArgs e)
        {
            WpfGames games = new WpfGames();
            games.Show();
        }
        private void BtnPlayers_Click(object sender, RoutedEventArgs e)
        {
            WpfPlayers players = new WpfPlayers();
            players.Show();
        }
        private void BtnTeams_Click(object sender, RoutedEventArgs e)
        {
            WpfTeams joukkueet = new WpfTeams();
            joukkueet.Show();
        }
        private void BtnEndprogram_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
