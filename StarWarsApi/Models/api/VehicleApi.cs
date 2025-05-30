using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsApi.Models.api
{
    public class VehicleApi
    {
        public string Uid { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
