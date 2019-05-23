using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;

namespace WpfSalibandyTournament
{
    public partial class DBSalibandytournament
    {
        private static string palvelin = WpfSalibandyTournament.Properties.Settings.Default.server;
        private static string tietokanta = WpfSalibandyTournament.Properties.Settings.Default.database;
        private static string salasana = WpfSalibandyTournament.Properties.Settings.Default.password;
        private static MySqlConnectionStringBuilder mySB = new MySqlConnectionStringBuilder()
        {
            Server = palvelin,
            Database = tietokanta,
            UserID = tietokanta,
            Password = salasana
        };
        public static DataTable GetViewDB(string viewname)
        {
            try
            {
                DataTable dt = new DataTable();
                using (MySqlConnection conn = new MySqlConnection(mySB.ConnectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"SELECT * FROM {viewname}", conn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void InsertIntoDB(string tablestring, string valuestring)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(mySB.ConnectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"INSERT INTO {tablestring} VALUES {valuestring}", conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void UpdateDB(string tablestring, string valuestring, string wherestring)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(mySB.ConnectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"UPDATE {tablestring} SET {valuestring} WHERE {wherestring}", conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
