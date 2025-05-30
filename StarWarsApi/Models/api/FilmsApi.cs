namespace StarWarsApi.Models.api
{
    public class Properties
    {
        public DateTime created { get; set; }
        public DateTime edited { get; set; }
        public List<string> starships { get; set; }
        public List<string> vehicles { get; set; }
        public List<string> planets { get; set; }
        public string producer { get; set; }
        public string title { get; set; }
        public int episode_id { get; set; }
        public string director { get; set; }
        public string release_date { get; set; }
        public string opening_crawl { get; set; }
        public List<string> characters { get; set; }
        public List<string> species { get; set; }
        public string url { get; set; }
    }

    public class FilmsApi
    {
        public Properties properties { get; set; }
        public string _id { get; set; }
        public string description { get; set; }
        public string uid { get; set; }
        public int __v { get; set; }
    }
}
