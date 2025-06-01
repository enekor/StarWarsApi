namespace StarWarsApi.Models.api
{

    public class FilmsListApi
    {
        public string message { get; set; }
        public List<FilmResult> result { get; set; }
        public string apiVersion { get; set; }
        public DateTime timestamp { get; set; }
        public Support support { get; set; }
        public Social social { get; set; }
    }



}
