using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsApi.Models.api
{
    public class CharacterApi
    {
        public string Uid { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        
    }
}
