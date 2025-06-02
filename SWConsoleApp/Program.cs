using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SWConsoleApp
{
    public class FilmDto
    {
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public List<string>? Starships { get; set; }
        public List<string>? Vehicles { get; set; }
        public List<string>? Planets { get; set; }
        public string? Producer { get; set; }
        public string? Title { get; set; }
        public int EpisodeId { get; set; }
        public string? Director { get; set; }
        public string? ReleaseDate { get; set; }
        public string? OpeningCrawl { get; set; }
        public List<string>? Characters { get; set; }
        public List<string>? Species { get; set; }
        public string? Url { get; set; }
    }

    class Program
    {
        static readonly string apiBase = "http://localhost:5000/Film"; // Cambia el puerto si es necesario
        static List<FilmDto> films = new();

        static async Task Main(string[] args)
        {
            await LoadFilms();
            if (films.Count == 0)
            {
                Console.WriteLine("No se encontraron películas.");
                return;
            }
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Películas disponibles:");
                for (int i = 0; i < films.Count; i++)
                    Console.WriteLine($"[{i + 1}] {films[i].Title}");
                Console.WriteLine("[0] Salir");
                Console.Write("Seleccione una película: ");
                if (!int.TryParse(Console.ReadLine(), out int sel) || sel < 0 || sel > films.Count)
                    continue;
                if (sel == 0) break;
                ShowFilmMenu(films[sel - 1]);
            }
        }

        static async Task LoadFilms()
        {
            using var client = new HttpClient();
            try
            {
                var resp = await client.GetAsync($"{apiBase}/GetFilms");
                resp.EnsureSuccessStatusCode();
                var json = await resp.Content.ReadAsStringAsync();
                films = JsonConvert.DeserializeObject<List<FilmDto>>(json) ?? new();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener películas: {ex.Message}");
            }
        }

        static void ShowFilmMenu(FilmDto film)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Título: {film.Title}");
                Console.WriteLine($"Director: {film.Director}");
                Console.WriteLine($"Productor: {film.Producer}");
                Console.WriteLine($"Fecha de estreno: {film.ReleaseDate}");
                Console.WriteLine($"Descripción: {film.Description}");
                Console.WriteLine($"Episodio: {film.EpisodeId}");
                Console.WriteLine($"Opening Crawl: {film.OpeningCrawl}");
                Console.WriteLine();
                Console.WriteLine("Opciones:");
                Console.WriteLine("[1] Ver personajes");
                Console.WriteLine("[2] Ver vehículos");
                Console.WriteLine("[3] Ver planetas");
                Console.WriteLine("[4] Ver especies");
                Console.WriteLine("[5] Ver starships");
                Console.WriteLine("[6] Guardar película en base de datos");
                Console.WriteLine("[0] Volver");
                Console.Write("Seleccione una opción: ");
                var opt = Console.ReadLine();
                if (opt == "0") break;
                switch (opt)
                {
                    case "1": ShowList("Personajes", film.Characters); break;
                    case "2": ShowList("Vehículos", film.Vehicles); break;
                    case "3": ShowList("Planetas", film.Planets); break;
                    case "4": ShowList("Especies", film.Species); break;
                    case "5": ShowList("Starships", film.Starships); break;
                    case "6": SaveFilm(film).Wait(); break;
                }
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
            }
        }

        static void ShowList(string label, List<string>? items)
        {
            Console.WriteLine($"\n{label}:");
            if (items == null || items.Count == 0)
                Console.WriteLine("(ninguno)");
            else
                foreach (var item in items)
                    Console.WriteLine("- " + item);
        }

        static async Task SaveFilm(FilmDto film)
        {
            using var client = new HttpClient();
            try
            {
                var json = JsonConvert.SerializeObject(film);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var resp = await client.PostAsync($"{apiBase}/SaveFilm", content);
                resp.EnsureSuccessStatusCode();
                Console.WriteLine("Película guardada correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar: {ex.Message}");
            }
        }
    }
}
