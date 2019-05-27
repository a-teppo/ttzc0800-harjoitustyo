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
        //get all players
        public static List<Player> GetPlayersFromDB()
        {
            List<Player> players = new List<Player>();
            DataTable dt = GetViewDB("Pelaajat");
            foreach (DataRow item in dt.Rows)
            {
                Player player = new Player()
                { 
                    HenkiloId = int.Parse(item[0].ToString()),
                    Sukunimi = item[1].ToString(),
                    Etunimi = item[2].ToString(),
                    Pelinumero = item[3].ToString(),
                    Pelipaikka = item[4].ToString(),
                    Syntymavuosi = int.Parse(item[5].ToString()),
                    Rooli = item[6].ToString(),
                    JoukkueId = int.Parse(item[7].ToString()),
                    Nimi = item[8].ToString()
                };
                players.Add(player);
            }
            return players;
        }
        //get players by team
        public static List<Player> GetTeamPlayersFromDB(int teamID)
        {
            List<Player> players = new List<Player>();
            string str = $"Pelaajat WHERE JoukkueID = {teamID} ORDER BY Pelinumero";
            DataTable dt = DBSalibandytournament.GetViewDB(str);
            foreach (DataRow item in dt.Rows)
            {
                if (item[3] != null)
                {
                    Player p = new Player()
                    {
                        HenkiloId = int.Parse(item[0].ToString()),
                        Pelinumero = item[3].ToString()
                    };
                    players.Add(p);
                }
            }
            return players;
        }
        public static List<Team> GetTeamsFromDB()
        {
            List<Team> teams = new List<Team>();
            DataTable dt = GetViewDB("Joukkueet");
            foreach (DataRow item in dt.Rows)
            {
                Team team = new Team()
                {
                    JoukkueId = int.Parse(item[0].ToString()),
                    Nimi = item[1].ToString(),
                    Paikkakunta = item[2].ToString(),
                    Seura = item[3].ToString()
                };
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
                    VierasMaalit = int.Parse(item[8].ToString()),
                    Paatetty = (bool)item[9]
                };
                games.Add(game);
            }
            return games;
        }
        public static List<Goal> GetGoalsFromDB(int gameID, int teamID)
        {
            List<Goal> goals = new List<Goal>();
            string str = $"Maali WHERE Ottelu = {gameID} AND Joukkue = {teamID} ORDER BY MaaliID";
            DataTable dt = DBSalibandytournament.GetViewDB(str);
            int index = 0;
            foreach (DataRow item in dt.Rows)
            {
                Goal g = new Goal()
                {
                    Maalinro = ++index,
                    MaaliID = int.Parse(item[0].ToString()),
                    Aika = item[1].ToString(),
                    Erikoistilanne = item[2].ToString(),
                    Maalintekija = int.Parse(item[3].ToString()),
                    Syottaja = ToNullableInt(item[4].ToString()),
                    Joukkue = int.Parse(item[5].ToString()),
                    Ottelu = int.Parse(item[6].ToString())
                };
                goals.Add(g);
            }
            return goals;
        }
        public static List<Penalty> GetPenaltiesFromDB(int gameID, int teamID)
        {
            List<Penalty> penalties = new List<Penalty>();
            string str = $"Rangaistus WHERE Ottelu = {gameID} AND Joukkue = {teamID} ORDER BY RangaistusID";
            DataTable dt = DBSalibandytournament.GetViewDB(str);
            int index = 0;
            foreach (DataRow item in dt.Rows)
            {
                Penalty p = new Penalty()
                {
                    Rangaistusnro = ++index,
                    RangaistusID = int.Parse(item[0].ToString()),
                    Aika = item[1].ToString(),
                    Kesto = int.Parse(item[2].ToString()),
                    Syy = item[3].ToString(),
                    Henkilo = int.Parse(item[4].ToString()),
                    Joukkue = int.Parse(item[5].ToString()),
                    Ottelu = int.Parse(item[6].ToString())
                };
                penalties.Add(p);
            }
            return penalties;
        }
        //convert string to nullable int
        public static int? ToNullableInt(string s)
        {
            if (int.TryParse(s, out int i)) return i;
            return null;
        }
    }
    public class Player
    {
        public int HenkiloId { get; set; }
        public string Sukunimi { get; set; }
        public string Etunimi { get; set; }
        public string Pelinumero { get; set; }
        public string Pelipaikka { get; set; }
        public int Syntymavuosi { get; set; }
        public string Rooli { get; set; }
        public string Nimi { get; set; }
        public int JoukkueId { get; set; }
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
        public bool Paatetty { get; set; }
    }
    public class Goal
    {
        public int MaaliID { get; set; }
        public int Maalinro { get; set; }
        public string Aika { get; set; }
        public string Erikoistilanne { get; set; }
        public int Maalintekija { get; set; }
        public int? Syottaja { get; set; }
        public int Ottelu { get; set; }
        public int Joukkue { get; set; }
    }
    public class Penalty
    {
        public int Rangaistusnro { get; set; }
        public int RangaistusID { get; set; }
        public string Aika { get; set; }
        public int Kesto { get; set; }
        public string Syy { get; set; }
        public int Henkilo { get; set; }
        public int Joukkue { get; set; }
        public int Ottelu { get; set; }
    }
}
