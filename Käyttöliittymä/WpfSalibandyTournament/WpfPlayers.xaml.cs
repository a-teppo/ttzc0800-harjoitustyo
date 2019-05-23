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
    /// Interaction logic for WpfPlayers.xaml
    /// </summary>
    public partial class WpfPlayers : Window
    {
        private List<Player> players = DBSalibandytournament.GetPlayersFromDB();

        public WpfPlayers()
        {
            InitializeComponent();
            dgPlayers.ItemsSource = players;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnNewperson_Click(object sender, RoutedEventArgs e)
        {
            WpfPlayereditor playereditor = new WpfPlayereditor();
            playereditor.ShowDialog();
            Close();
        }
    }
}
