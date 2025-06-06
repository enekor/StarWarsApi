﻿using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsApi.Models.api
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
   public class StarshipProperties
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
        public List<object> pilots { get; set; }
        public string MGLT { get; set; }
        public string starship_class { get; set; }
        public string hyperdrive_rating { get; set; }
        public List<string> films { get; set; }
        public string url { get; set; }
    }

    public class StarshipResult
    {
        public StarshipProperties properties { get; set; }
        public string _id { get; set; }
        public string description { get; set; }
        public string uid { get; set; }
        public int __v { get; set; }
    }

    public class StarshipApi
    {
        public string message { get; set; }
        public StarshipResult result { get; set; }
        public string apiVersion { get; set; }
        public DateTime timestamp { get; set; }
        public Support support { get; set; }
        public Social social { get; set; }
    }


}
