using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace WpfSalibandyTournament
{
    public partial class DBSalibandytournament
    {
        public static List<Player> GetPlayersFromDB()
        {
            List<Player> players = new List<Player>();
            DataTable dt = GetPlayersDB();
            foreach (DataRow item in dt.Rows)
            {
                Player player = new Player();
                player.HenkiloId = int.Parse(item[0].ToString());
                player.Sukunimi = item[1].ToString();
                player.Etunimi = item[2].ToString();
                player.Syntymavuosi = int.Parse(item[3].ToString());
                player.Nimi = item[4].ToString();

                players.Add(player);
            }
            return players;
        }

        public static List<Team> GetTeamsFromDB()
        {
            List<Team> teams = new List<Team>();
            DataTable dt = GetTeamsDB();
            foreach (DataRow item in dt.Rows)
            {
                Team team = new Team();
                team.JoukkueId = int.Parse(item[0].ToString());
                team.Nimi = item[1].ToString();
                team.Paikkakunta = item[2].ToString();
                team.Seura = item[3].ToString();

                teams.Add(team);
            }

            return teams;
        }
    }

    public class Player
    {
        public int HenkiloId { get; set; }
        public string Sukunimi { get; set; }
        public string Etunimi { get; set; }
        public int Syntymavuosi { get; set; }
        public string Nimi { get; set; }
    }

    public class Team
    {
        public int JoukkueId { get; set; }
        public string Nimi { get; set; }
        public string Paikkakunta { get; set; }
        public string Seura { get; set; }
        public string LogoURL { get; set; }

    }
}
