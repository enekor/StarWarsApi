using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsApi.Models.api
{
    public class PlanetApi
    {
        public string Uid { get; set; }
        public string Name { get; set; }
        public string url { get; set; }
    }
}
