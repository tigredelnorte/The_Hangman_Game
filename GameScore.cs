using System;
using System.Collections.Generic;

namespace The_Hangman_Game
{
    [Serializable]
    public class GameScore : IComparable<GameScore>
    {
        public string PlayerName { get; set; }
        public DateTime GameTime { get; set; }
        public int GameDuration { get; set; }
        public int GuessingAttempts { get; set; }
        public Country Country { get; set; }

        public GameScore(string name, DateTime gameTime, int duration, int attempts, Country country)
        {
            PlayerName = name;
            GameTime = gameTime;
            GameDuration = duration;
            GuessingAttempts = attempts;
            Country = country;
        }

        public override string ToString()
        {
            return "| " + PlayerName + " | " + GameTime.Day + "." + GameTime.Month + "." + GameTime.Year + " | " +
                GameTime.Hour + ":" + GameTime.Minute + " | " + GuessingAttempts + " | " + GameDuration + " | " +
                Country.Capital + " |";
            //return "Player name: " + PlayerName + "\n" +
            //    "Game Date: " + GameTime.Day + "." + GameTime.Month + "." + GameTime.Year +"\n" +
            //    "Game Time: " + GameTime.Hour + ":" + GameTime.Minute + "\n" +
            //    "Number of attempts: " + GuessingAttempts + "\n" +
            //    "Game Duration: " + GameDuration + " seconds\n" +
            //    "Guessed Capital: " + Country.Capital + "\n";
        }

        public int CompareTo(GameScore other)
        {
            if (this.GameDuration.CompareTo(other.GameDuration) == 0)
            {
                if(this.GuessingAttempts.CompareTo(other.GuessingAttempts) == 0)
                {
                    return 1;
                }
                else
                {
                    return this.GuessingAttempts.CompareTo(other.GuessingAttempts);
                }
            }
            return this.GameDuration.CompareTo(other.GameDuration);
        }

        internal void print(int v)
        {
            Console.WriteLine("| {0, -3} | {1,-13} | {2,-10} | {3,-9} | {4,-8} | {5,-8} | {6,-14} |", v.ToString(), PlayerName, 
                GameTime.ToString("dd.MM.yyy"),GameTime.ToString("HH:mm"), GameDuration + " sec.", GuessingAttempts, Country.Capital);
        }

        public override int GetHashCode()
        {
            int hashCode = -1704595645;
            hashCode = hashCode * -1521134295 + GameDuration.GetHashCode();
            hashCode = hashCode * -1521134295 + GuessingAttempts.GetHashCode();
            return hashCode;
        }

        public int SortByNumberOfAttempts(int nr1, int nr2)
        {
            return nr1.CompareTo(nr2);
        }
    }
}