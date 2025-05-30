namespace StarWarsApi.Services
{
    using BDCADAO.BDModels;
    using Microsoft.EntityFrameworkCore;

    public class BaseService
    {
        protected readonly string _connectionString = "https://www.swapi.tech/api/";
        protected readonly ModelContext _context;
        public BaseService(ModelContext context)
        {
            _context = context;
        }
    }
}