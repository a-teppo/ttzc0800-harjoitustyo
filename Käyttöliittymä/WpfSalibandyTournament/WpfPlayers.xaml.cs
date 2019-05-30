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
    /// Interaction logic for WpfPlayers.xaml
    /// </summary>
    public partial class WpfPlayers : Window
    {
        private List<Player> players { get; set; }       
        public WpfPlayers()
        {
            InitializeComponent();
            //show playerlist in datagrid
            RefreshPlayers();
            
        }
        public void RefreshPlayers()
        {
            this.players = DBSalibandytournament.GetPlayersFromDB();
            dgPlayers.ItemsSource = players;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void btnNewperson_Click(object sender, RoutedEventArgs e)
        {
            WpfPlayereditor playereditor = new WpfPlayereditor(this);
            playereditor.ShowDialog();            
        }
        private void btnEditPerson_Click(object sender, RoutedEventArgs e)
        {
            Player selectedPlayer = (Player)dgPlayers.SelectedItem;
            if(selectedPlayer != null)
            {
                WpfPlayereditor playereditor = new WpfPlayereditor(this, selectedPlayer);
                playereditor.ShowDialog();
            }
            RefreshPlayers();
        }
    }
}
