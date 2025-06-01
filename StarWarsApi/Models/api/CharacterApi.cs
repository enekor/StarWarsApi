using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsApi.Models.api
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
   // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    

    public class CharacterProperties
    {
        public DateTime created { get; set; }
        public DateTime edited { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string skin_color { get; set; }
        public string hair_color { get; set; }
        public string height { get; set; }
        public string eye_color { get; set; }
        public string mass { get; set; }
        public string homeworld { get; set; }
        public string birth_year { get; set; }
        public string url { get; set; }
    }

    public class CharacterResult
    {
        public CharacterProperties properties { get; set; }
        public string _id { get; set; }
        public string description { get; set; }
        public string uid { get; set; }
        public int __v { get; set; }
    }

    public class CharacterApi
    {
        public string message { get; set; }
        public CharacterResult result { get; set; }
        public string apiVersion { get; set; }
        public DateTime timestamp { get; set; }
        public Support support { get; set; }
        public Social social { get; set; }
    }
}
