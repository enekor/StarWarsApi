using Microsoft.EntityFrameworkCore;
using StarWarsApi.Models.database;

namespace StarWarsApi.Daos
{
    public class StarshipDao:BaseDao<Starship>
    {
        public StarshipDao(DbSet<Starship> dbset) : base(dbset) { }
    }
}
