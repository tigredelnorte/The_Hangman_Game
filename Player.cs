using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Hangman_Game
{
    [Serializable]
    public class Player
    {
        public string Name { get; set; }
        public List<GameScore> GamesScoreList { get; set; }

        public Player(string name)
        {
            Name = name;
            GamesScoreList = new List<GameScore>();
        }

        public override string ToString()
        {
            return "Name: " + Name 
                + "\nGameScoreList: " +  GamesScoreList.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is Player player &&
                   Name == player.Name &&
                   EqualityComparer<List<GameScore>>.Default.Equals(GamesScoreList, player.GamesScoreList);
        }

        public override int GetHashCode()
        {
            int hashCode = 874035752;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<GameScore>>.Default.GetHashCode(GamesScoreList);
            return hashCode;
        }
    }
}
