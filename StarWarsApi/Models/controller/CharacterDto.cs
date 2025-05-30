using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsApi.Models.controller
{
    public class CharacterDto
    {

        public string Name { get; set; }

        public string Url { get; set; }

        
    }
}
