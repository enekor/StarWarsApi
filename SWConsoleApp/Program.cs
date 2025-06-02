using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SWConsoleApp.Models;

namespace SWConsoleApp
{

    class Program
    {
        static readonly string apiBase = "https://localhost:7079/Film"; // Cambia a http://localhost:5212/Film si usas HTTP
        static List<FilmsDto> films = new();
        static Random random = new();
        static readonly string[] loadingMessages = new[]
        {
            "Reparando espadas láser...",
            "Apuntando a las estrellas...",
            "Acariciando a Chewbacca...",
            "Cargando midiclorianos...",
            "Despertando a R2-D2...",
            "Convocando a la Fuerza...",
            "Evitando el lado oscuro...",
            "Saltando al hiperespacio..."
        };

        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("¿Desde dónde quieres ver las películas?");
                Console.WriteLine("[1] SWApi (API externa)");
                Console.WriteLine("[2] Base de datos local");
                Console.WriteLine("[0] Salir");
                Console.Write("Selecciona una opción: ");
                var opt = Console.ReadLine();
                if (opt == "0") return;
                if (opt == "1")
                {
                    await LoadFilms(fromApi: true);
                    break;
                }
                if (opt == "2")
                {
                    await LoadFilms(fromApi: false);
                    break;
                }
            }
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

        static async Task LoadFilms(bool fromApi)
        {
            using var client = new HttpClient();
            try
            {
                ShowLoading();
                string endpoint = fromApi ? $"{apiBase}/GetFilms" : $"{apiBase}/GetFilmsFromDatabase";
                var resp = await client.GetAsync(endpoint);
                resp.EnsureSuccessStatusCode();
                var json = await resp.Content.ReadAsStringAsync();
                films = JsonConvert.DeserializeObject<List<FilmsDto>>(json) ?? new();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener películas: {ex.Message}");
            }
        }

        static void ShowLoading()
        {
            var msg = loadingMessages[random.Next(loadingMessages.Length)];
            Console.WriteLine($"\n{msg}\n");
        }

        static void ShowFilmMenu(FilmsDto film)
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
                    case "1": ShowList("Personajes", film.Characters?.Where(v => v?.Name != null).Select(v => v!.Name!).ToList() ?? new List<string>()); break;
                    case "2": ShowList("Vehículos", film.Vehicles?.Where(v => v?.Name != null).Select(v => v!.Name!).ToList() ?? new List<string>()); break;
                    case "3": ShowList("Planetas", film.Planets?.Where(v => v?.Name != null).Select(v => v!.Name!).ToList() ?? new List<string>()); break;
                    case "4": ShowList("Especies", film.Species?.Where(v => v?.Name != null).Select(v => v!.Name!).ToList() ?? new List<string>()); break;
                    case "5": ShowList("Starships", film.Starships?.Where(v => v?.Name != null).Select(v => v!.Name!).ToList() ?? new List<string>()); break;
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

        static async Task SaveFilm(FilmsDto film)
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
