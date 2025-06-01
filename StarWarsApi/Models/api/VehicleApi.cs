using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsApi.Models.api
{
    public class VehicleProperties
    {
        public DateTime created { get; set; }
        public DateTime edited { get; set; }
        public string consumables { get; set; }
        public string name { get; set; }
        public string cargo_capacity { get; set; }
        public string passengers { get; set; }
        public string max_atmosphering_speed { get; set; }
        public string crew { get; set; }
        public string length { get; set; }
        public string model { get; set; }
        public string cost_in_credits { get; set; }
        public string manufacturer { get; set; }
        public string vehicle_class { get; set; }
        public List<object> pilots { get; set; }
        public List<string> films { get; set; }
        public string url { get; set; }
    }

    public class VehicleResult
    {
        public VehicleProperties properties { get; set; }
        public string _id { get; set; }
        public string description { get; set; }
        public string uid { get; set; }
        public int __v { get; set; }
    }

    public class VehicleApi
    {
        public string message { get; set; }
        public VehicleResult result { get; set; }
        public string apiVersion { get; set; }
        public DateTime timestamp { get; set; }
        public Support support { get; set; }
        public Social social { get; set; }
    }
}
