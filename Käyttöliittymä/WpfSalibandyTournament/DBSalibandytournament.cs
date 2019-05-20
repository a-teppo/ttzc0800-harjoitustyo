﻿using System;
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

        public static DataTable GetPlayersDB()
        {
            try
            {
                DataTable dt = new DataTable();
                string palvelin = WpfSalibandyTournament.Properties.Settings.Default.server;
                string tietokanta = WpfSalibandyTournament.Properties.Settings.Default.database;
                string salasana = WpfSalibandyTournament.Properties.Settings.Default.password;

                MySqlConnectionStringBuilder mySB = new MySqlConnectionStringBuilder();
                mySB.Server = palvelin;
                mySB.Database = tietokanta;
                mySB.UserID = tietokanta;
                mySB.Password = salasana;

                using (MySqlConnection conn = new MySqlConnection(mySB.ConnectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Pelaajat", conn);
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


    }
}
