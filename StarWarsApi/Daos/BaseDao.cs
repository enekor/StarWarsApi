using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StarWarsApi.Models.database;

namespace StarWarsApi.Daos
{
    public class BaseDao<T> where T : BaseModel
    {
        protected DatabaseFacade? _adatabase;

        protected DbSet<T>? _dbquery;

        public BaseDao() { }
        public BaseDao(DbSet<T> aquery, DatabaseFacade? addb = null)
        {
            _dbquery = aquery;
            _adatabase = addb;
        }

        /// <summary>
        /// obtiene todos los datos de la clase especificada
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll()
        {
            return _dbquery.Distinct().AsNoTracking().ToList();
        }

        public T InsertOrUpdate(T obj)
        {
            if(_dbquery.Where(x=> x.Uid == obj.Uid).Any())
            {
                return _dbquery.Update(obj).Entity;
            }
            else
            {
                return _dbquery.Add(obj).Entity;
            }
        }

        public int Delete(string id)
        {
            return _dbquery.Where(x => x.Uid == id).ExecuteDelete();
        }

    }
}
