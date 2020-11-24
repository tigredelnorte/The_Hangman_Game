using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace The_Hangman_Game
{
    public class Data
    {
        public static List<Country> LoadCountries(string file, string gameLevel)
        {
            string Europe = "Albania, Andorra, Armenia, Austria, Azerbaijan, Belarus, Belgium, " +
                "Bosnia and Herzegovina, Bulgaria, Croatia, Cyprus, Czech Republic, Denmark, " +
                "Estonia, Finland, France, Georgia, Germany, Greece, Hungary, Iceland, Ireland, " +
                "Italy, Kosovo, Latvia, Liechtenstein, Lithuania, Luxembourg, Macedonia, Malta, " +
                "Moldova, Monaco, Montenegro, Netherlands, Norway, Poland, Portugal, Romania, " +
                "Russia, San Marino, Serbia, Slovakia, Slovenia, Spain, Sweden, Switzerland, " +
                "Ukraine, United Kingdom";
            string Asia = "Afghanistan, Bahrain, Bangladesh, Bhutan, Brunei, Cambodia, China, " +
                "India, Indonesia, Iran, Iraq, Israel, Japan, Jordan, Kazakhstan, Kuwait, " +
                "Kyrgyzstan, Laos, Lebanon, Malaysia, Maldives, Mongolia, Nepal, North Korea, " +
                "Oman, Pakistan, Palestine, Philippines, Qatar, Saudi Arabia, Singapore, " +
                "South Korea, Syria, Taiwan, Tajikistan, Thailand, Turkey, Turkmenistan, " +
                "United Arab Emirates, Uzbekistan, Vietnam";
            string Africa = "Algeria, Angola, Botswana, Burkina Faso, Burundi, Cabo Verde, " +
                "Cameroon, Central African Republic, Comoros, Democratic Republic of the Congo, " +
                "Republic of the Congo, Egypt, Equatorial Guinea, Eritrea, Ethiopia, Gabon, " +
                "Gambia, Ghana, Guinea, Guinea-Bissau, Kenya, Lesotho, Liberia, Libya, Madagascar, " +
                "Malawi, Mali, Mauritania, Mauritius, Morocco, Mozambique, Namibia, Niger, Nigeria, " +
                "Rwanda, Senegal, Seychelles, Sierra Leone, Somalia, South Africa, South Sudan, " +
                "Sudan, Swaziland, Tanzania, Togo, Tunisia, Uganda, Zambia, Zimbabwe";
            string Americas = "Argentina, Bahamas, Barbados, Belize, Bolivia, Brazil, Canada, " +
                "Chile, Colombia, Costa Rica, Cuba, Dominica, Dominican Republic, Ecuador, " +
                "El Salvador, Guatemala, Guyana, Honduras, Jamaica, Mexico, Nicaragua, Panama, " +
                "Paraguay, Peru, Saint Kitts and Nevis, Saint Lucia, Saint Vincent and the Grenadines, " +
                "Suriname, Trinidad and Tobago, United States of America, Uruguay, Venezuela";
            string Oceania = "Australia, Fiji, Kiribati, Marshall Islands, Micronesia, Nauru, " +
                "New Zealand, Palau, Papua New Guinea, Samoa, Solomon Islands, Tuvalu, Vanuatu";

            var countries = new List<Country>();
            using (StreamReader reader= new StreamReader(file))
            {
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] loadData = Regex.Split(line, "[ ][|][ ]");
                    switch (gameLevel)
                    {
                        case "WORLD":
                            countries.Add(new Country(loadData[0], loadData[1], gameLevel));
                            break;
                        case "EUROPE":
                            if (Europe.Contains(loadData[0]))
                            {
                                countries.Add(new Country(loadData[0], loadData[1], gameLevel));
                            }
                            break;
                        case "AFRICA":
                            if (Africa.Contains(loadData[0]))
                            {
                                countries.Add(new Country(loadData[0], loadData[1], gameLevel));
                            }
                            break;
                        case "ASIA":
                            if (Asia.Contains(loadData[0]))
                            {
                                countries.Add(new Country(loadData[0], loadData[1], gameLevel));
                            }
                            break;
                        case "AMERICAS":
                            if (Americas.Contains(loadData[0]))
                            {
                                countries.Add(new Country(loadData[0], loadData[1], gameLevel));
                            }
                            break;
                        case "OCEANIA":
                            if (Oceania.Contains(loadData[0]))
                            {
                                countries.Add(new Country(loadData[0], loadData[1], gameLevel));
                            }
                            break;
                    }
                }
            }
            return countries;
        }
    }
}
