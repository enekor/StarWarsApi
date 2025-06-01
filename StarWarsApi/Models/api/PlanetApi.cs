using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsApi.Models.api
{
    public class PlanetProperties
    {
        public DateTime created { get; set; }
        public DateTime edited { get; set; }
        public string climate { get; set; }
        public string surface_water { get; set; }
        public string name { get; set; }
        public string diameter { get; set; }
        public string rotation_period { get; set; }
        public string terrain { get; set; }
        public string gravity { get; set; }
        public string orbital_period { get; set; }
        public string population { get; set; }
        public string url { get; set; }
    }

    public class PlanetResult
    {
        public PlanetProperties properties { get; set; }
        public string _id { get; set; }
        public string description { get; set; }
        public string uid { get; set; }
        public int __v { get; set; }
    }

    public class PLanetApu
    {
        public string message { get; set; }
        public PlanetResult result { get; set; }
        public string apiVersion { get; set; }
        public DateTime timestamp { get; set; }
        public Support support { get; set; }
        public Social social { get; set; }
    }
}
