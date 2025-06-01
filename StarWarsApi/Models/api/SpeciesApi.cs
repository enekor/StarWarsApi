using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsApi.Models.api

{
    public class SpecieProperties
    {
        public DateTime created { get; set; }
        public DateTime edited { get; set; }
        public string classification { get; set; }
        public string name { get; set; }
        public string designation { get; set; }
        public string eye_colors { get; set; }
        public List<string> people { get; set; }
        public string skin_colors { get; set; }
        public string language { get; set; }
        public string hair_colors { get; set; }
        public string homeworld { get; set; }
        public string average_lifespan { get; set; }
        public string average_height { get; set; }
        public string url { get; set; }
    }

    public class SpecieResult
    {
        public SpecieProperties properties { get; set; }
        public string _id { get; set; }
        public string description { get; set; }
        public string uid { get; set; }
        public int __v { get; set; }
    }

    public class SpecieApi
    {
        public string message { get; set; }
        public SpecieResult result { get; set; }
        public string apiVersion { get; set; }
        public DateTime timestamp { get; set; }
        public Support support { get; set; }
        public Social social { get; set; }
    }


}
