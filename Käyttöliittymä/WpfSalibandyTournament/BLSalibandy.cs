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
            DataTable dt = GetViewDB("Pelaajat");
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
            DataTable dt = GetViewDB("Joukkueet");
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
        public static List<Game> GetGamesFromDB()
        {
            List<Game> games = new List<Game>();
            DataTable dt = GetViewDB("Ottelut");
            foreach (DataRow item in dt.Rows)
            {
                Game game = new Game()
                {
                    OtteluId = int.Parse(item[0].ToString()),
                    Aika = item[1].ToString(),
                    Paikka = item[2].ToString(),
                    KotiId = int.Parse(item[3].ToString()),
                    KotiNimi = item[4].ToString(),
                    VierasId = int.Parse(item[5].ToString()),
                    VierasNimi = item[6].ToString(),
                    KotiMaalit = int.Parse(item[7].ToString()),
                    VierasMaalit = int.Parse(item[8].ToString())
                };
                games.Add(game);
            }
            return games;
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
    public class Game
    {
        public int OtteluId { get; set; }
        public string Aika { get; set; }
        public string Paikka { get; set; }
        public int KotiId { get; set; }
        public string KotiNimi { get; set; }
        public int VierasId { get; set; }
        public string VierasNimi { get; set; }
        public int KotiMaalit { get; set; }
        public int VierasMaalit { get; set; }
    }
}
