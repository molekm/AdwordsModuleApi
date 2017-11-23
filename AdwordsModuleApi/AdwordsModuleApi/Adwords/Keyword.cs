using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls.WebParts;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using ParallelDots;

namespace AdwordsModuleApi.Adwords
{
    public static class Keyword
    {
        private static api Pd = new api("hkltvSknQTawfl9IRr1Rg7CQzohHZR3T9ZIxfsMVjGk");

        public static string[] GetKeyWords(string keywords)
        {
            //object result = new List<KeyValueKeywords>();
            keywords = keywords.Replace(",", "").Replace(" ", ",");
            var outpKeywords = Pd.keywords("Mørk mahognifarvet øl med et tæt nøddebrunt skum, der har en lang holdbarhed og giver fine blonder på glasset." +
                                           "Dejlig duft af røgmalt og engelsk lakrids. Fyldig maltet smag med en ekstra tone af røgmalt og engelsk lakrids, der fint" +
                                           "afrundes i en efter øltypen frisk afslutning.Dette er med til at give en helt særlig smagsoplevelse." +
                                           "Fur øl brygges med professionel omhu og respekt for ædle råvarer. Engelsk inspireret Porter, der er brygget på 5 forskellige malttyper, hvor nogle" +
                                           "af dem er røgmalt, karamelmalt og chokolademalt. FUR PORTER er brygget på 2 humletyper, hvoraf den ene specielle humletype" +
                                           "er med til at give øllet et friskt pift.Humlen tilsættes ad 2 gange. FUR PORTER er brygget af vand fra Fur som er forædlet gennem tusinder af" +
                                           "år.Langsomt er vandet sivet ned gennem moler, aflejret på havbunden for mere end 55 millioner år siden.Filtreret gennem 200 lag af vulkansk aske fra" +
                                           "Nordatlantens åbning. Furvandet giver FUR øl den helt særlige, bløde karakter og er en af hemmelighederne i den fyldige, vedholdende skumkrone, der kendetegner" +
                                           "bryggeriets øl. Fur Porter er uden tilsætningsstoffer.");

            var example2Model = new JavaScriptSerializer().Deserialize<KeyValueKeywords>(outpKeywords);

            return new string[] {"hej" };
        } 
    }

    public class KeyValueKeywords
    {
        public List<Keywords> keywords { get; set; }
    }

    public class Keywords
    {
        public string keyword { get; set; }
        public double confidence_score { get; set; }
    }
}