using Microsoft.EntityFrameworkCore;
using StarWarsApi.Models.database;

namespace StarWarsApi.Daos
{
    public class VehicleDao:BaseDao<Vehicle>
    {
        public VehicleDao(DbSet<Vehicle> dbset) : base(dbset) { }
    }
}
