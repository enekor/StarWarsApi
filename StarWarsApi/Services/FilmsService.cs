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

        public FilmsService(HttpClient httpClient, ModelContext context) : base(context,httpClient)
        {
        }

        public async Task<List<FilmsDto>> GetAllFilmsAsync()
        {
            CharacterService characterService = new CharacterService(_httpClient, _context);
            VehicleService vehicleService = new VehicleService(_httpClient, _context);
            StarshipService starshipService = new StarshipService(_httpClient, _context);
            PlanetsService planetService = new PlanetsService(_httpClient, _context);
            SpeciesService speciesService = new SpeciesService(_httpClient, _context);

            var response = await _httpClient.GetAsync($"{_connectionString}/films");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var films = JsonSerializer.Deserialize<List<FilmsApi>>(content);

            var returnerFilms = new List<FilmsDto>();

            foreach (FilmsApi film in films)
            {
                List<CharacterDto> characters = new List<CharacterDto>();
                List<VehicleDto> vehicles = new List<VehicleDto>();
                List<StarshipDto> starships = new List<StarshipDto>();
                List<PlanetDto> planets = new List<PlanetDto>();
                List<SpeciesDto> species = new List<SpeciesDto>();

                if (film.properties.characters != null)
                {
                    characters = await Task.WhenAll(
                        film.properties.characters.Select(c => characterService.GetCharacterByIdAsync(c.Split('/').Last()))
                    ).ContinueWith(t => t.Result.ToList());
                }
                if (film.properties.vehicles != null)
                {
                    vehicles = await Task.WhenAll(
                        film.properties.vehicles.Select(v => vehicleService.GetVehicleByIdAsync(v.Split('/').Last()))
                    ).ContinueWith(t => t.Result.ToList());
                }
                if (film.properties.starships != null)
                {
                    starships = await Task.WhenAll(
                        film.properties.starships.Select(s => starshipService.GetStarshipByIdAsync(s.Split('/').Last()))
                    ).ContinueWith(t => t.Result.ToList());
                }
                if (film.properties.planets != null)
                {
                    planets = await Task.WhenAll(
                        film.properties.planets.Select(p => planetService.GetPlanetByIdAsync(p.Split('/').Last()))
                    ).ContinueWith(t => t.Result.ToList());
                }
                if (film.properties.species != null)
                {
                    species = await Task.WhenAll(
                        film.properties.species.Select(s => speciesService.GetSpeciesByIdAsync(s.Split('/').Last()))
                    ).ContinueWith(t => t.Result.ToList());
                }

                returnerFilms.Add(ACFilmMapper.Instance.MapToController(film, starships, characters, vehicles, planets, species));
            }


            return returnerFilms;
        }

        public async Task<FilmsDto> GetFilmByIdAsync(string id)
        {
            CharacterService characterService = new CharacterService(_httpClient, _context);
            VehicleService vehicleService = new VehicleService(_httpClient, _context);
            StarshipService starshipService = new StarshipService(_httpClient, _context);
            PlanetsService planetService = new PlanetsService(_httpClient, _context);
            SpeciesService speciesService = new SpeciesService(_httpClient, _context);

            List<CharacterDto> characters = new List<CharacterDto>();
            List<VehicleDto> vehicles = new List<VehicleDto>();
            List<StarshipDto> starships = new List<StarshipDto>();
            List<PlanetDto> planets = new List<PlanetDto>();
            List<SpeciesDto> species = new List<SpeciesDto>();

            var response = await _httpClient.GetAsync($"{_connectionString}/films/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var film = JsonSerializer.Deserialize<FilmsApi>(content);


            if (film.properties.characters != null)
            {
                characters = await Task.WhenAll(
                    film.properties.characters.Select(c => characterService.GetCharacterByIdAsync(c.Split('/').Last()))
                ).ContinueWith(t => t.Result.ToList());
            }
            if (film.properties.vehicles != null)
            {
                vehicles = await Task.WhenAll(
                    film.properties.vehicles.Select(v => vehicleService.GetVehicleByIdAsync(v.Split('/').Last()))
                ).ContinueWith(t => t.Result.ToList());
            }
            if (film.properties.starships != null)
            {
                starships = await Task.WhenAll(
                    film.properties.starships.Select(s => starshipService.GetStarshipByIdAsync(s.Split('/').Last()))
                ).ContinueWith(t => t.Result.ToList());
            }
            if (film.properties.planets != null)
            {
                planets = await Task.WhenAll(
                    film.properties.planets.Select(p => planetService.GetPlanetByIdAsync(p.Split('/').Last()))
                ).ContinueWith(t => t.Result.ToList());
            }
            if (film.properties.species != null)
            {
                species = await Task.WhenAll(
                    film.properties.species.Select(s => speciesService.GetSpeciesByIdAsync(s.Split('/').Last()))
                ).ContinueWith(t => t.Result.ToList());
            }


            return ACFilmMapper.Instance.MapToController(film, starships, characters, vehicles, planets, species);
        }
        public async Task SaveFilmAsync(FilmsDto filmDto)
        {
            var film = await GetFilmByIdAsync(filmDto.Url.Split("/").Last());

            foreach (var character in filmDto.Characters)
            {
                _context.Characters.InsertOrUpdate(CDCharacterMapper.Instance.ToEntity(character));
                await _context.SaveChangesAsync();
            }

            foreach (var vehicle in filmDto.Vehicles)
            {
                _context.Vehicles.InsertOrUpdate(CDVehicleMapper.Instance.ToEntity(vehicle));
                await _context.SaveChangesAsync();
            }

            foreach (var starship in filmDto.Starships)
            {
                _context.Starships.InsertOrUpdate(CDStarshipMapper.Instance.ToEntity(starship));
                await _context.SaveChangesAsync();
            }

            foreach (var planet in filmDto.Planets)
            {
                _context.Planets.InsertOrUpdate(CDPlanetMapper.Instance.ToEntity(planet));
                await _context.SaveChangesAsync();
            }

            foreach (var species in filmDto.Species)
            {
                _context.Species.InsertOrUpdate(CDSpecieMapper.Instance.ToEntity(species));
                await _context.SaveChangesAsync();
            }

            _context.Films.InsertOrUpdate(CDFilmMapper.Instance.ToEntity(filmDto));
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteFilmAsync(string id)
        {
            int deleted = _context.Films.Delete(id);

            return deleted > 0;
        }        public List<FilmsDto> GetFilmsFromDB()
        {
            // Create necessary services
            CharacterService characterService = new CharacterService(_httpClient, _context);
            VehicleService vehicleService = new VehicleService(_httpClient, _context);
            StarshipService starshipService = new StarshipService(_httpClient, _context);
            PlanetsService planetService = new PlanetsService(_httpClient, _context);
            SpeciesService speciesService = new SpeciesService(_httpClient, _context);

            // Get all films from database
            var films = _context.Films.GetAll();
            var filmDtos = new List<FilmsDto>();

            foreach (var film in films)
            {                // Split and get characters
                List<String> characterIds = film.Characters?.Split(',').ToList() ?? new List<string>();
                var characters = characterIds.Select(id => characterService.GetCharacterFromDB(id)).ToList();

                // Split and get vehicles
                List<String> vehicleIds = film.Vehicles?.Split(',').ToList() ?? new List<string>();
                var vehicles = vehicleIds.Select(id => vehicleService.GetVehicleFromDB(id)).ToList();

                // Split and get starships
                List<String> starshipIds = film.Starships?.Split(',').ToList() ?? new List<string>();
                var starships = starshipIds.Select(id => starshipService.GetStarshipFromDB(id)).ToList();

                // Split and get planets
                List<String> planetIds = film.Planets?.Split(',').ToList() ?? new List<string>();
                var planets = planetIds.Select(id => planetService.GetPlanetFromDB(id)).ToList();

                // Split and get species
                List<String> speciesIds = film.Species?.Split(',').ToList() ?? new List<string>();
                var species = speciesIds.Select(id => speciesService.GetSpecieFromDB(id)).ToList();

                // Map to DTO with all related data
                filmDtos.Add(DCFilmMapper.Instance.ToDto(film, starships, characters, vehicles, planets, species));
            }

            return filmDtos;
        }
    }
}