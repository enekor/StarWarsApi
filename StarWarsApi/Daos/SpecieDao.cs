using Microsoft.EntityFrameworkCore;
using StarWarsApi.Models.database;

namespace StarWarsApi.Daos
{
    public class SpecieDao:BaseDao<Species>
    {
        public SpecieDao(DbSet<Species> dbset) : base(dbset)
        {
        }

    }
}
