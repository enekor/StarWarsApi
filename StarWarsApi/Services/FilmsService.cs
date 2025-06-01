using StarWarsApi.Models;
using StarWarsApi.Mappers;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using StarWarsApi.Models.controller;
using BDCADAO.BDModels;
using StarWarsApi.Mappers.FromApiToController;
using StarWarsApi.Models.api;
using StarWarsApi.Mappers.FromDatabaseToController;
using StarWarsApi.Mappers.FromApiToDatabase;
using StarWarsApi.Daos;
using StarWarsApi.Mappers.FromControllerToDatabase;

namespace StarWarsApi.Services
{
    public class FilmsService : BaseService
    {
        private readonly CharacterService _characterService;
        private readonly VehicleService _vehicleService;
        private readonly StarshipService _starshipService;
        private readonly PlanetsService _planetService;
        private readonly SpeciesService _speciesService;

        public FilmsService(HttpClient httpClient, ModelContext context) : base(context, httpClient)
        {
            _characterService = new CharacterService(httpClient, context);
            _vehicleService = new VehicleService(httpClient, context);
            _starshipService = new StarshipService(httpClient, context);
            _planetService = new PlanetsService(httpClient, context);
            _speciesService = new SpeciesService(httpClient, context);
        }

        public async Task<List<FilmsDto>> GetAllFilmsAsync()
        {
            var response = await _httpClient.GetAsync($"{_connectionString}/films");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var films = JsonSerializer.Deserialize<FilmsListApi>(content);
            
            var filmDtos = new List<FilmsDto>();
            if (films == null)
                return filmDtos;


            foreach (var film in films.result)
            {
                var filmDto = ACFilmMapper.Instance.MapToController(film);
                if (filmDto != null)
                {
                    await LoadFilmRelations(filmDto, new()
                    {
                        result = film

                    });
                    filmDtos.Add(filmDto);
                }
            }

            return filmDtos;
        }

        public async Task<FilmsDto?> GetFilmByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{_connectionString}/films/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var film = JsonSerializer.Deserialize<FilmApi>(content);

            if (film?.result == null)
                return null;

            var filmDto = ACFilmMapper.Instance.MapToController(film);
            if (filmDto != null)
            {
                await LoadFilmRelations(filmDto, film);
            }

            return filmDto;
        }

        private async Task LoadFilmRelations(FilmsDto filmDto, FilmApi film)
        {
            // Cargar personajes
            if (film.result?.properties?.characters != null)
            {
                filmDto.Characters = (await Task.WhenAll(
                    film.result.properties.characters
                        .Select(c => _characterService.GetCharacterByIdAsync(c.Split('/').Last()))
                )).Where(c => c != null).ToList();
            }

            // Cargar vehículos
            if (film.result?.properties?.vehicles != null)
            {
                filmDto.Vehicles = (await Task.WhenAll(
                    film.result.properties.vehicles
                        .Select(v => _vehicleService.GetVehicleByIdAsync(v.Split('/').Last()))
                )).Where(v => v != null).ToList();
            }

            // Cargar naves
            if (film.result?.properties?.starships != null)
            {
                filmDto.Starships = (await Task.WhenAll(
                    film.result.properties.starships
                        .Select(s => _starshipService.GetStarshipByIdAsync(s.Split('/').Last()))
                )).Where(s => s != null).ToList();
            }

            // Cargar planetas
            if (film.result?.properties?.planets != null)
            {
                filmDto.Planets = (await Task.WhenAll(
                    film.result.properties.planets
                        .Select(p => _planetService.GetPlanetByIdAsync(p.Split('/').Last()))
                )).Where(p => p != null).ToList();
            }

            // Cargar especies
            if (film.result?.properties?.species != null)
            {
                filmDto.Species = (await Task.WhenAll(
                    film.result.properties.species
                        .Select(s => _speciesService.GetSpeciesByIdAsync(s.Split('/').Last()))
                )).Where(s => s != null).ToList();
            }
        }

        public async Task SaveFilmAsync(FilmsDto filmDto)
        {
            try 
            {
                var film = CDFilmMapper.Instance.ToEntity(filmDto);
                if (film == null) return;

                // Asignar un ID si no tiene uno
                if (string.IsNullOrEmpty(film.Uid))
                {
                    film.Uid = filmDto.Url?.Split('/').Last() ?? Guid.NewGuid().ToString();
                }

                // Guardar entidades relacionadas
                if (filmDto.Characters?.Any() == true)
                {
                    foreach(var character in filmDto.Characters)
                    {
                        if (string.IsNullOrEmpty(character.Url))
                        {
                            character.Url = $"https://swapi.dev/api/people/{Guid.NewGuid()}";
                        }
                        await _characterService.SaveCharacterAsync(character);
                    }
                    film.Characters = string.Join(",", filmDto.Characters.Select(c => c.Url?.Split('/').Last()));
                }

                if (filmDto.Vehicles?.Any() == true)
                {
                    foreach (var vehicle in filmDto.Vehicles)
                    {
                        if (string.IsNullOrEmpty(vehicle.Url))
                        {
                            vehicle.Url = $"https://swapi.dev/api/vehicles/{Guid.NewGuid()}";
                        }
                        await _vehicleService.SaveVehicleAsync(vehicle);
                    }
                    film.Vehicles = string.Join(",", filmDto.Vehicles.Select(v => v.Url?.Split('/').Last()));
                }

                if (filmDto.Starships?.Any() == true)
                {
                    foreach (var starship in filmDto.Starships)
                    {
                        if (string.IsNullOrEmpty(starship.Url))
                        {
                            starship.Url = $"https://swapi.dev/api/starships/{Guid.NewGuid()}";
                        }
                        await _starshipService.SaveStarshipAsync(starship);
                    }
                    film.Starships = string.Join(",", filmDto.Starships.Select(s => s.Url?.Split('/').Last()));
                }

                if (filmDto.Planets?.Any() == true)
                {
                    foreach (var planet in filmDto.Planets)
                    {
                        if (string.IsNullOrEmpty(planet.Url))
                        {
                            planet.Url = $"https://swapi.dev/api/planets/{Guid.NewGuid()}";
                        }
                        await _planetService.SavePlanetAsync(planet);
                    }
                    film.Planets = string.Join(",", filmDto.Planets.Select(p => p.Url?.Split('/').Last()));
                }

                if (filmDto.Species?.Any() == true)
                {
                    foreach (var species in filmDto.Species)
                    {
                        if (string.IsNullOrEmpty(species.Url))
                        {
                            species.Url = $"https://swapi.dev/api/species/{Guid.NewGuid()}";
                        }
                        await _speciesService.SaveSpeciesAsync(species);
                    }
                    film.Species = string.Join(",", filmDto.Species.Select(s => s.Url?.Split('/').Last()));
                }

                _context.Films.InsertOrUpdate(film);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar la película: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteFilmAsync(string id)
        {
            int deleted = _context.Films.Delete(id);
            await _context.SaveChangesAsync();
            return deleted > 0;
        }

        public FilmsDto? GetFilmFromDB(string id)
        {
            var film = _context.Films.GetById(id);
            if (film == null)
                return null;

            var filmDto = DCFilmMapper.Instance.ToDto(film);
            if (filmDto == null)
                return null;

            // Cargar las entidades relacionadas desde la base de datos
            if (!string.IsNullOrEmpty(film.Characters))
            {
                filmDto.Characters = film.Characters.Split(',')
                    .Select(cid => _characterService.GetCharacterFromDB(cid))
                    .Where(c => c != null)
                    .ToList();
            }

            if (!string.IsNullOrEmpty(film.Vehicles))
            {
                filmDto.Vehicles = film.Vehicles.Split(',')
                    .Select(vid => _vehicleService.GetVehicleFromDB(vid))
                    .Where(v => v != null)
                    .ToList();
            }

            if (!string.IsNullOrEmpty(film.Starships))
            {
                filmDto.Starships = film.Starships.Split(',')
                    .Select(sid => _starshipService.GetStarshipFromDB(sid))
                    .Where(s => s != null)
                    .ToList();
            }

            if (!string.IsNullOrEmpty(film.Planets))
            {
                filmDto.Planets = film.Planets.Split(',')
                    .Select(pid => _planetService.GetPlanetFromDB(pid))
                    .Where(p => p != null)
                    .ToList();
            }

            if (!string.IsNullOrEmpty(film.Species))
            {
                filmDto.Species = film.Species.Split(',')
                    .Select(sid => _speciesService.GetSpecieFromDB(sid))
                    .Where(s => s != null)
                    .ToList();
            }

            return filmDto;
        }

        public List<FilmsDto> GetFilmsFromDB()
        {
            return DCFilmMapper.Instance.ToDtoList(_context.Films.GetAll());
        }
    }
}