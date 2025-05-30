using Microsoft.EntityFrameworkCore;
using StarWarsApi.Models.database;

namespace StarWarsApi.Daos
{
    public class PlanetDao:BaseDao<Planet>
    {

        public PlanetDao(DbSet<Planet> dbset):base(dbset) { }
    }
}
