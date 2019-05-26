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
    /// Interaction logic for WpfTeams.xaml
    /// </summary>
    public partial class WpfTeams : Window
    {
        private List<Team> teams = DBSalibandytournament.GetTeamsFromDB();

        public WpfTeams()
        {
            InitializeComponent();
            dgTeams.ItemsSource = teams;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnNewteam_Click(object sender, RoutedEventArgs e)
        {
            WpfTeameditor teameditor = new WpfTeameditor();
            teameditor.ShowDialog();
        }

        private void BtnEditteam_Click(object sender, RoutedEventArgs e)
        {
            Team selectedTeam = (Team)dgTeams.SelectedItem;
            if (selectedTeam != null)
            {
                WpfTeameditor teameditor = new WpfTeameditor(selectedTeam);
                teameditor.ShowDialog();
            }
            
            Close();
        }
    }

}
