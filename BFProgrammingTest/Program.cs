using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFProgrammingTest
{
    class Program
    {

        static void Main(string[] args)
        {
            
            string connectionString = @"Data Source=Frido-PC\SQLEXPRESS;Initial Catalog=BFProgrammingTest;Integrated Security=SSPI"; ;
            int startRange = 0;
            int endRange = 10;
            int correctNumber = 5;
            int numberOfAttempts = 3;

            DatabaseManager dbManager = new DatabaseManager();

            if (dbManager.InitialiseDatabase(connectionString))
            {
                NumberManager numberManager = new NumberManager(dbManager);

                if (numberManager.StartNumberGame(startRange, endRange, correctNumber, numberOfAttempts))
                {
                    Console.WriteLine($"Correct number. Good job!");
                }
                else
                {
                    Console.WriteLine($"No more attempts remaining.");
                }

                dbManager.CloseDbConnection();
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

    }
}
