using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using SharedLibrary;

namespace Server.DAL
{
    class DBConnection
    {
        public SqlConnection Connection;
        public FlightsDBContext Context;
        public readonly string ConnectionString = 
            @"Server=" + Config.DBServerIP 
            + ";Database=" + Config.DBServerDatabase 
            + ";User Id=" + Config.DBServerUsername 
            + ";Password=" + Config.DBServerPassword;
        private DBConnection()
        {
            try
            {
                Context = new FlightsDBContext();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private static DBConnection instance;
        public static DBConnection Instance
        {
            get
            {
                if(instance== null)
                {
                    instance = new DBConnection();
                }
                return instance;
            }
        }
    }
}
