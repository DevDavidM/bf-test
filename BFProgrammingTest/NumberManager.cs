using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFProgrammingTest
{
    class NumberManager
    {
        private DatabaseManager _databaseManager;

        internal NumberManager(DatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        /// <summary>
        /// Starts the number game and returns success status
        /// </summary>
        /// <param name="startRange"></param>
        /// <param name="numberOfAttempts"></param>
        /// <returns></returns>
        internal bool StartNumberGame(int startRange, int endRange, int correctNumber, int numberOfAttempts)
        {
            for (int i = 1; i <= numberOfAttempts; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"Attempt {i}.");

                int numberEntered = GetNumber(startRange, endRange);
                if (numberEntered == correctNumber)
                {
                    _databaseManager.LogAttempt(i, true);
                    return true;
                }
                else
                {
                    Console.WriteLine($"Wrong number. Attempts remaining: {numberOfAttempts - i}");
                    _databaseManager.LogAttempt(i, false);
                }
            }

            return false;
        }



        private int GetNumber(int startRange, int endRange)
        {
            string textEntered;

            while (true)
            {
                Console.WriteLine($"Please enter a number between {startRange} and {endRange}, then press enter.");

                textEntered = Console.ReadLine();

                if (Validate(textEntered, startRange, endRange))
                {
                    break;
                }
            }

            return int.Parse(textEntered);
        }

        private bool Validate(string textEntered, int startRange, int endRange)
        {
            int numberEntered = 0;
            if (!int.TryParse(textEntered, out numberEntered))
            {
                Console.WriteLine($"'{textEntered}' is not a number");
                return false;
            }

            if (numberEntered < startRange)
            {
                Console.WriteLine($"'{textEntered}' is below the start range of {startRange}");
                return false;
            }
            if (numberEntered > endRange)
            {
                Console.WriteLine($"'{textEntered}' is above the end range of {endRange}");
                return false;
            }

            return true;
        }
    }
}
