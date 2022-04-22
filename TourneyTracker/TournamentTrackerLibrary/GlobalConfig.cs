using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentTrackerLibrary.DataAccess;
using System.Configuration;

namespace TournamentTrackerLibrary
{
    public static class GlobalConfig
    {
        public static IDataConnection Connection { get; set; }

        public static void InitializeConnection()
        {
            SqlConnector sql = new SqlConnector();
            Connection = sql;
        }
        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public static string AppKeyLookUp(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
