using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFProgrammingTest
{
    public class DatabaseManager
    {
        private SqlConnection Cn;
        private SqlDataAdapter Adapter = new SqlDataAdapter();



        internal bool InitialiseDatabase(string connectionString)
        {
            if (!OpenDbConnection(connectionString))
            {
                return false;
            }

            //Add potential DB validation

            return true;
        }

        internal bool OpenDbConnection(string connectionString)
        {
            //Make sure, that there is no existing connection. If there is, close it.
            CloseDbConnection();

            Console.WriteLine("Connecting to Database...");

            Cn = new SqlConnection(connectionString);
            try
            {
                Cn.Open();

            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error connection to the Database.");
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            Console.WriteLine("Connected!");
            return true;
        }

        internal void CloseDbConnection()
        {
            if (Cn != null && Cn.State != System.Data.ConnectionState.Closed)
            {
                Cn.Close();
            }
        }

        internal void LogAttempt(int attempt, bool success)
        {
            if (Cn == null ||Cn.State != System.Data.ConnectionState.Open)
            {
                return;
            }

            string bitBoolean = success ? "1" : "0";

            string sql = $"INSERT INTO dbo.Attempt (AttemptNumber, Success) VALUES ({attempt}, {bitBoolean})";

            Adapter.InsertCommand = new SqlCommand(sql, Cn);
            Adapter.InsertCommand.ExecuteNonQuery();
        }

    }
}
