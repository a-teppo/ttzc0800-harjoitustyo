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
        private List<Game> games;
        public WpfGames()
        {
            InitializeComponent();
            RefreshGames();
        }
        public void RefreshGames()
        {
            this.games = DBSalibandytournament.GetGamesFromDB();
            dgGames.ItemsSource = games;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            WpfGameEditor gameEditor = new WpfGameEditor(this);
            gameEditor.ShowDialog();
        }
        private void btnOpenGame_Click(object sender, RoutedEventArgs e)
        {
            Game selectedGame = (Game)dgGames.SelectedItem;
            if (selectedGame != null)
            {
                WpfGamestatistics stats = new WpfGamestatistics(this, selectedGame);
                stats.ShowDialog();
            }
        }
        private void btnEditGame_Click(object sender, RoutedEventArgs e)
        {
            Game selectedGame = (Game)dgGames.SelectedItem;
            if (selectedGame != null)
            {
                WpfGameEditor gameEditor = new WpfGameEditor(this, selectedGame);
                gameEditor.ShowDialog();
            }
        }
    }
}
