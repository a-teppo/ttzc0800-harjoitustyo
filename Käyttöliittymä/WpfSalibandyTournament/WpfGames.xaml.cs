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
    /// Interaction logic for WpfGames.xaml
    /// </summary>
    public partial class WpfGames : Window
    {
        private List<Game> games = DBSalibandytournament.GetGamesFromDB();
        public WpfGames()
        {
            InitializeComponent();
            dgGames.ItemsSource = games;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            Game selectedGame = (Game)dgGames.SelectedItem;
            if (selectedGame != null)
            {
                WpfGamestatistics stats = new WpfGamestatistics(selectedGame);
                stats.ShowDialog();
            }
            Close();
        }
    }
}
