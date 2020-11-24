using System;

namespace The_Hangman_Game
{
    [Serializable]
    public class Country
    {
        public string Name { get; set; }
        public string Capital { get; set; }
        public string Continent { get; set; }

        public Country(string name, string capital, string continent)
        {
            Name = name;
            Capital = capital;
            Continent = continent;
        }
    }
}
