using Microsoft.AspNetCore.Mvc;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;
using StarWarsApi.Services;

namespace StarWarsApi.Controllers
{
    public class VehicleController : ControllerBase
    {
        private readonly VehicleService _vehicleService;

        public VehicleController(VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet("/[controller]/GetVehicles")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            return Ok(vehicles);
        }

        [HttpPost("/[controller]/SaveVehicle")]
        public async Task<ActionResult<Vehicle>> CreateVehicle([FromBody] VehicleDto vehicle)
        {
             _vehicleService.SaveVehicleAsync(vehicle);
            return CreatedAtAction("Insert vehicle", vehicle);
        }

        [HttpDelete("/[controller]/DeleteVehicle/{id}")]
        public async Task<IActionResult> DeleteVehicle(string id)
        {
            await _vehicleService.DeleteVehicleAsync(id);
            return NoContent();
        }
    }
}