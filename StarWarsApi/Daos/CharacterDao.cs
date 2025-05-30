using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using StarWarsApi.Models.database;

namespace StarWarsApi.Daos
{
    public class CharacterDao:BaseDao<Character>
    {
        public CharacterDao(DbSet<Character> dbset) : base(dbset) { }
    }
}
