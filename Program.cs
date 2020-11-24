using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace The_Hangman_Game
{
    class Program
    {
        private const string file = "countries_and_capitals.txt";
        static string level = "EUROPE";
        static void Main(string[] args)
        {

            var countries = Data.LoadCountries(file, level);
            int randomNr = new Random().Next(0, countries.Count());
            Game game = new Game(countries.ElementAt(randomNr));
            game.start();
            bool gameOn = true;
            while (gameOn)
            {
                Console.Clear();
                Console.WriteLine("DO YOU WANT TO PLAY ANOTHER GAME: (Y)ES / (N)O?");
                string input = Console.ReadLine().ToUpper();
                if (input.Equals("YES") || input.Equals("Y"))
                {
                    countries = LoadNextGame();
                    randomNr = new Random().Next(0, countries.Count());
                    game = new Game(countries.ElementAt(randomNr));
                    game.start();
                }
                else if (input.Equals("NO") || input.Equals("N"))
                {
                    gameOn = false;
                }           
            }
        }
        private static List<Country> LoadNextGame()
        {
            Console.Clear();
            Console.WriteLine("SELECT NEXT GAME CAPITALS' AREA:\n"
                + "1. EUROPE\n2. ASIA\n3. AFRICA\n" 
                + "4. AMERICA\n5. OCEANIA\n6. WORLD");
            var countries = new List<Country>();
            switch (int.Parse(Console.ReadLine()))
            {
                case 1:
                    countries = Data.LoadCountries(file, "EUROPE");
                    break;
                case 2:
                    countries = Data.LoadCountries(file, "ASIA");
                    break;
                case 3:
                    countries = Data.LoadCountries(file, "AFRICA");
                    break;
                case 4:
                    countries = Data.LoadCountries(file, "AMERICAS");
                    break;
                case 5:
                    countries = Data.LoadCountries(file, "OCEANIA");
                    break;
                case 6:
                    countries = Data.LoadCountries(file, "WORLD");
                    break;
            }
            return countries;
        }
    }
}
