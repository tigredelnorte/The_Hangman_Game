using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;

namespace The_Hangman_Game
{
    internal class Game
    {
        public Country Country { get; set; }
        public string answer { get; set; }
        public int PlayerLife { get; set; }
        public List<char> CharsNotInWord { get; set; }
        public int GuessCounter { get; set; }
        public string GameLevel { get; set; }
        public int GuessWordCounter { get; set; }


        public Game(Country country)
        {
            Country = country;
            string[] temp = Regex.Split(Country.Capital, "\\s+");
            answer = new string('_', Country.Capital.Length);
            for (int i = 0; i < Country.Capital.Length; i++)
            {
                if (Country.Capital[i].Equals(' '))
                {
                    answer = answer.Substring(0, i) + ' ' + answer.Substring(i + 1);
                }
            }
            CharsNotInWord = new List<char>();
            PlayerLife = 6;
            GuessCounter = 0;
            GameLevel = AssignGameLevel(Country.Continent);
            GuessWordCounter = 0;
        }

        public void start()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool GameInProgress = true;
            Console.Clear();
            Console.WriteLine("####################");
            Console.WriteLine("# THE HANGMAN GAME #");
            Console.WriteLine("####################\n");
            Console.WriteLine("PRESS ENTER TO START...");
            Console.ReadLine();
            while (GameInProgress)
            {
                printGameStatus();
                Regex regex = new Regex("[12]");
                String input = Console.ReadLine();
                while (!regex.IsMatch(input))
                {
                    Console.Write("\nINCORRECT INPUT. PLEASE SELECT 1 OR 2: ");
                    input = Console.ReadLine();
                }
                switch (int.Parse(input))
                {
                    case 1:
                        {
                            if (PlayerLife == 1)
                            {
                                Console.WriteLine("\nYOU MAY NEED A SMALL HINT?"
                                    + "\nTHE CAPITAL OF " + Country.Name.ToUpper() + " IS...?");
                            }
                            Console.Write("\nENTER A LETTER: ");
                            Regex regex1 = new Regex("^[a-zA-Z]$");
                            String inputChar = Console.ReadLine();
                            while (!regex1.IsMatch(inputChar))
                            {
                                Console.Write("\nINCORRECT INPUT. PLEASE ENTER A LETTER FROM A TO Z: ");
                                inputChar = Console.ReadLine();
                            }
                            while (CharsNotInWord.Contains(char.Parse(inputChar.ToUpper()))
                                || answer.Contains(char.Parse(inputChar.ToUpper()))
                                || answer.Contains(char.Parse(inputChar.ToLower())))
                            {
                                Console.Write("\nYOU ALREADY ASKED FOR THIS LETTER. PICK ANOTHER ONE: ");
                                inputChar = Console.ReadLine();
                                while (!regex1.IsMatch(inputChar))
                                {
                                    Console.Write("\nINCORRECT INPUT. PLEASE ENTER A LETTER FROM A TO Z: ");
                                    inputChar = Console.ReadLine();
                                }
                            }
                            GuessCounter++;
                            bool letterNotFound = true;
                            StringBuilder sb = new StringBuilder();
                            for (int i = 0; i < Country.Capital.Length; i++)
                            {
                                if (string.Equals(inputChar, Country.Capital[i].ToString(), StringComparison.OrdinalIgnoreCase))
                                {
                                    sb.Append(answer)[i] = Country.Capital[i];
                                    answer = sb.ToString();
                                    sb.Clear();
                                    letterNotFound = false;
                                }
                            }
                            if (letterNotFound)
                            {
                                PlayerLife = PlayerLife - 1;
                                if (PlayerLife < 1)
                                {
                                    GameInProgress = false;
                                }
                                CharsNotInWord.Add(char.Parse(inputChar.ToUpper()));
                                Console.WriteLine("\nINCORRECT LETTER. YOU LOST 1 POINT OF LIFE!");
                                Console.Write("PRESS ENTER TO CONTINUE...");
                                Console.ReadLine();
                            }
                            else
                            {
                                if (answer.Equals(Country.Capital))
                                {
                                    GameInProgress = false;
                                }
                                else
                                {
                                    Console.WriteLine("\n!!!GOOD SHOT!!!\n!!!WELL DONE!!!\n");
                                }
                                Console.Write("\nPRESS ENTER TO CONTINUE...");
                                Console.ReadLine();
                            }
                            break;
                        }

                    case 2:
                        if (PlayerLife <= 2)
                        {
                            Console.WriteLine("\nYOU MAY NEED A SMALL HINT?"
                                + "\nTHE CAPITAL OF " + Country.Name.ToUpper() + " IS...?");
                        }
                        Console.Write("\nENTER YOUR ANSWER: ");
                        string inputString = Console.ReadLine();
                        while (inputString.Equals(""))
                        {
                            Console.Write("\nINCORRECT INPUT. PLEASE TRY AGAIN: ");
                            inputString = Console.ReadLine();
                        }
                        if (string.Equals(inputString, Country.Capital, StringComparison.OrdinalIgnoreCase))
                        {
                            GameInProgress = false;
                            Console.WriteLine("\n!!! CORRECT ANSWER !!!");
                        }
                        else
                        {
                            PlayerLife = PlayerLife - 2;
                            if (PlayerLife < 1)
                            {
                                GameInProgress = false;
                            }
                            Console.WriteLine("\nINCORRECT ANSWER! YOU LOST 2 POINTS OF LIFE!");
                        }
                        GuessWordCounter++;
                        Console.Write("\nPRESS ENTER TO CONTINUE...");
                        Console.ReadLine();
                        break;
                }
                if (!GameInProgress)
                {
                    if (PlayerLife > 0)
                    {
                        sw.Stop();
                        printHangmanEnd();
                        print(Country.Capital);
                        Console.WriteLine("\n!!!YOU WON!!!\n!!!CONGRATULATIONS!!!");
                        Console.Write("\nYOU GUESSED THE CAPITAL OF " + Country.Name.ToUpper() + " AFTER " + GuessCounter + " LETTER(S)");
                        if (GuessWordCounter > 0) Console.WriteLine(" AND " + GuessWordCounter + " WORD(S) ATTEMPT(S).");
                        Console.Write("\nIT TOOK YOU " + (int) sw.ElapsedMilliseconds / 1000 + " SECONDS\n");
                        SaveGameStats((int) sw.ElapsedMilliseconds / 1000);
                    }
                    else if (PlayerLife < 1)
                    {
                        Console.Clear();
                        printHangman();
                        Console.WriteLine("!!! YOU LOST ALL YOUR LIFE !!!\n!!! GAME OVER !!!\n");
                        Console.WriteLine("THE CORRECT ANSWER WAS " + Country.Capital.ToUpper() + " THE CAPITAL OF " + Country.Name.ToUpper());
                        Console.Write("\nPRESS ENTER TO CONTINUE...");
                        Console.ReadLine();
                    }
                    PrintHallOfFame();
                }
            }
        }

        private void PrintHallOfFame()
        {
            Console.Clear();
            Console.WriteLine("!!! THE HANGMAN GAME !!!\n!!! HALL OF FAME !!!\n");
            List<Player> playersList = loadListOfPlayers();
            List<GameScore> TotalList = new List<GameScore>();
            foreach (Player player in playersList)
            {
                TotalList.AddRange(player.GamesScoreList);
            }
            printHallTop10(TotalList);
            Console.Write("\nPRESS ENTER TO CONTINUE...");
            Console.ReadLine();
        }

        private void SaveGameStats(int seconds)
        {
            Console.Write("\nPLEASE GIVE YOUR NAME TO SAVE GAME RESULTS: ");
            string playerName = getPlayerInput();
            List<Player> playersList = loadListOfPlayers();
            Player player = loadPlayer(playersList, playerName);
            GameScore game = new GameScore(player.Name, DateTime.Now, seconds, GuessCounter + GuessWordCounter, Country);
            player.GamesScoreList.Add(game);
            if (playersList.Contains(player))
            {
                playersList.Remove(player);
            }
            playersList.Add(player);
            printTopScores(player, game);
            saveListOfPlayers(playersList);
        }


        private void printHallTop10(List<GameScore> totalList)
        {
            totalList.Sort();
            Console.WriteLine("| Nr  | Player Name   | Game Date  | Game Time | Duration | Attempts | Capital        |");
            Console.WriteLine("---------------------------------------------------------------------------------------");
            for (int i = 0; i < Math.Min(10, totalList.Count); i++)
            {
                totalList.ElementAt(i).print(i + 1);
            }
        }

        private void printTopScores(Player player, GameScore game)
        {
            List<GameScore> gs = player.GamesScoreList;
            gs.Sort();
            Console.Clear();
            Console.WriteLine("!!! " + player.Name + " TOP 10 GAMES !!!\n");
            Console.WriteLine("| Nr  | Player Name   | Game Date  | Game Time | Duration | Attempts | Capital        |");
            Console.WriteLine("---------------------------------------------------------------------------------------");
            for (int i = 0; i < Math.Min(10, gs.Count); i++)
            {
                gs.ElementAt(i).print(i + 1);
            }
            if (gs.Count > 10 && gs.IndexOf(game) >= 10)
            {
                Console.WriteLine("---------------------------------------------------------------------------------------");
                game.print(gs.IndexOf(game) + 1);
            }
        }

        private void saveListOfPlayers(List<Player> playersList)
        {
            try
            {
                Serializator.Serialize("ListOfPlayers.txt", playersList);
                Console.WriteLine("\nPRESS ENTER TO CONTINUE...");
            }
            catch (Exception e)
            {
                Console.WriteLine("\nUPPSSSSSS");
                Console.WriteLine("\nPRESS ENTER TO CONTINUE...");
            }
            Console.ReadLine();
        }

        private List<Player> loadListOfPlayers()
        {
            try
            {
                return (List<Player>)Serializator.Deserialize("ListOfPlayers.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine("\nCREATING NEW PLAYERS LIST\n");
                return new List<Player>();
            }
        }

        private Player loadPlayer(List<Player> playersList, string playerName)
        {
            Player player;
            if (playersList.Where(i => i.Name == playerName).Any())
            {
                player = playersList.Where(i => i.Name == playerName).First();
            }
            else
            {
                player = new Player(playerName);
                Console.Clear();
                Console.WriteLine("\nNEW PLAYER: " + playerName + " CREATED");
                Console.WriteLine("PRESS ENTER TO CONTINUE...");
                Console.ReadLine();
            }
            return player;
        }

        private string getPlayerInput()
        {
            string input = Console.ReadLine();
            while (input.Equals(""))
            {
                Console.Write("\nINCORRECT INPUT. PLEASE TRY AGAIN... ");
                input = Console.ReadLine();
            }
            return input;
        }

        private void printGameStatus()
        {
            Console.Clear();
            Console.WriteLine("GUESS THE NAME OF ONE OF " + GameLevel + " CAPITALS BEFORE YOU LOOSE ALL YOU LIFE\n");
            Console.WriteLine("YOUR LIFE " + PlayerLife + "/6");
            if (CharsNotInWord.Count() > 0)
            {
                CharsNotInWord.Sort();
                Console.Write("\nLETTERS NOT IN WORD\n<< ");
                foreach (char ch in CharsNotInWord)
                {
                    Console.Write(ch + " ");
                }
                Console.Write(">>\n");
            }
            printHangman();
            //print(Country.Capital);
            print(answer);
            Console.WriteLine("\nWHAT WOULD YOU LIKE TO GUESS:\n" +
                "1. LETTER (WRONG ANSER WILL COST 1 LIFE POINT)\n" +
                "2. CAPITAL NAME (WRONG ANSER WILL COST 2 LIFE POINTS)");
        }

        private void printHangmanEnd()
        {
            Console.Clear();
            Console.WriteLine(" \n"
                + "      ________\n"
                + "     |/     \\|\n"
                + "     |                         \\  0  /\n"
                + "     |                          \\ | /\n"
                + "     |                           \\|/\n"
                + "     |                            |\n"
                + "     |                            |\n"
                + "     |                           / \\\n"
                + "  ___|________________          /   \\\n"
                + " /   |               /|        /     \\\n"
                + "/    |              / /\n"
                + "___________________/ /\n"
                + "___________________|/\n");
        }

        private void printHangman()
        {
            switch (PlayerLife)
            {
                case 0:
                    Console.WriteLine(" \n"
                        + "      ________\n"
                        + "     |/     \\|\n"
                        + "     |       | \n"
                        + "     |       ()\n"
                        + "     |      /|\\\n"
                        + "     |     / | \\\n"
                        + "     |    /  |  \\\n"
                        + "     |      /\\\n"
                        + "  ___|_____/__\\______\n"
                        + " /   |    /    \\     /|\n"
                        + "/    |              / /\n"
                        + "___________________/ /\n"
                        + "___________________|/\n");
                    break;
                case 1:
                    Console.WriteLine(" \n"
                        + "      ________\n"
                        + "     |/     \\|\n"
                        + "     |    \\  0  /\n"
                        + "     |     \\ | /\n"
                        + "     |      \\|/\n"
                        + "     |       |\n"
                        + "     |       |\n"
                        + "     |      /  \n"
                        + "  ___|_____/__________\n"
                        + " /   |    /           /|\n"
                        + "/    |              / /\n"
                        + "___________________/ /\n"
                        + "___________________|/\n");
                    break;
                case 2:
                    Console.WriteLine(" \n"
                        + "      ________\n"
                        + "     |/     \\|\n"
                        + "     |    \\  0  /\n"
                        + "     |     \\ | /\n"
                        + "     |      \\|/\n"
                        + "     |       |\n"
                        + "     |       |\n"
                        + "     |         \n"
                        + "  ___|________________\n"
                        + " /   |                /|\n"
                        + "/    |              / /\n"
                        + "___________________/ /\n"
                        + "___________________|/\n");
                    break;
                case 3:
                    Console.WriteLine(" \n"
                        + "      ________\n"
                        + "     |/     \\|\n"
                        + "     |    \\  0\n"
                        + "     |     \\ |\n"
                        + "     |      \\|\n"
                        + "     |       |\n"
                        + "     |       |\n"
                        + "     |         \n"
                        + "  ___|________________\n"
                        + " /   |                /|\n"
                        + "/    |              / /\n"
                        + "___________________/ /\n"
                        + "___________________|/\n");
                    break;
                case 4:
                    Console.WriteLine(" \n"
                        + "      ________\n"
                        + "     |/     \\|\n"
                        + "     |       0\n"
                        + "     |       | \n"
                        + "     |       |\n"
                        + "     |       |\n"
                        + "     |       |\n"
                        + "     |         \n"
                        + "  ___|________________\n"
                        + " /   |                /|\n"
                        + "/    |              / /\n"
                        + "___________________/ /\n"
                        + "___________________|/\n");
                    break;
                case 5:
                    Console.WriteLine(" \n"
                        + "      ________\n"
                        + "     |/     \\|\n"
                        + "     |       0\n"
                        + "     |         \n"
                        + "     |        \n"
                        + "     |      \n"
                        + "     |       \n"
                        + "     |         \n"
                        + "  ___|________________\n"
                        + " /   |                /|\n"
                        + "/    |              / /\n"
                        + "___________________/ /\n"
                        + "___________________|/\n");
                    break;
                case 6:
                    Console.WriteLine(" \n"
                        + "      ________\n"
                        + "     |/     \\|\n"
                        + "     |          \n"
                        + "     |         \n"
                        + "     |        \n"
                        + "     |      \n"
                        + "     |       \n"
                        + "     |         \n"
                        + "  ___|________________\n"
                        + " /   |                /|\n"
                        + "/    |              / /\n"
                        + "___________________/ /\n"
                        + "___________________|/\n");
                    break;
            }
        }

        public void print(string name)
        {
            for (int i = 0; i < name.Length; i++)
            {
                if (Country.Capital[i].Equals(' '))
                {
                    Console.Write("  ");
                }
                else
                {
                    Console.Write(name[i] + " ");
                }
            }
            Console.WriteLine();
        }

        private string AssignGameLevel(string gameLevel)
        {
            string continentName = "***** ***";
            switch (gameLevel.ToUpper())
            {
                case "EUROPE":
                    continentName = "EUROPEAN";
                    break;
                case "ASIA":
                    continentName = "ASIAN";
                    break;
                case "AFRICA":
                    continentName = "AFRICAN";
                    break;
                case "AMERICAS":
                    continentName = "AMERICAN";
                    break;
                case "OCEANIA":
                    continentName = "OCEANIAN";
                    break;
                case "WORLD":
                    continentName = "WORLD";
                    break;
            }
            return continentName;
        }
    }
}