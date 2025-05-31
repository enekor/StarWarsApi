namespace StarWarsApi.Services
{
    using BDCADAO.BDModels;
    using Microsoft.EntityFrameworkCore;

    public class BaseService
    {
        protected readonly string _connectionString = "https://www.swapi.tech/api/";
        protected readonly ModelContext _context;
        protected readonly HttpClient _httpClient;
        public BaseService(ModelContext context, HttpClient httpClient)
        {
            _httpClient = httpClient;
       
            _context = context;
        }
    }
}