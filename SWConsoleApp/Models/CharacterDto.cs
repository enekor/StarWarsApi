﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWConsoleApp.Models
{
    public class CharacterDto
    {
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? SkinColor { get; set; }
        public string? HairColor { get; set; }
        public string? Height { get; set; }
        public string? EyeColor { get; set; }
        public string? Mass { get; set; }
        public string? Homeworld { get; set; }
        public string? BirthYear { get; set; }
        public string? Url { get; set; }
    }
}
