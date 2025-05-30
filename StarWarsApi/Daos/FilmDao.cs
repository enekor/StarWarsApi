using Microsoft.EntityFrameworkCore;
using StarWarsApi.Models.database;

namespace StarWarsApi.Daos
{
    public class FilmDao:BaseDao<Films>
    {
        public FilmDao(DbSet<Films> dbset) : base(dbset)
        {
        }
    }
}
