using BDCADAO.BDModels;
using Microsoft.EntityFrameworkCore;
using StarWarsApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext configuration
builder.Services.AddDbContext<ModelContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure HttpClient with base address
builder.Services.AddHttpClient("StarWarsApi", client =>
{
    client.BaseAddress = new Uri("https://www.swapi.tech/api/");
});

// Register services for dependency injection
builder.Services.AddScoped<CharacterService>(sp =>
    new CharacterService(sp.GetRequiredService<IHttpClientFactory>().CreateClient("StarWarsApi"),
                        sp.GetRequiredService<ModelContext>()));

builder.Services.AddScoped<FilmsService>(sp =>
    new FilmsService(sp.GetRequiredService<IHttpClientFactory>().CreateClient("StarWarsApi"),
                    sp.GetRequiredService<ModelContext>()));

builder.Services.AddScoped<PlanetsService>(sp =>
    new PlanetsService(sp.GetRequiredService<IHttpClientFactory>().CreateClient("StarWarsApi"),
                      sp.GetRequiredService<ModelContext>()));

builder.Services.AddScoped<SpeciesService>(sp =>
    new SpeciesService(sp.GetRequiredService<IHttpClientFactory>().CreateClient("StarWarsApi"),
                      sp.GetRequiredService<ModelContext>()));

builder.Services.AddScoped<StarshipService>(sp =>
    new StarshipService(sp.GetRequiredService<IHttpClientFactory>().CreateClient("StarWarsApi"),
                       sp.GetRequiredService<ModelContext>()));

builder.Services.AddScoped<VehicleService>(sp =>
    new VehicleService(sp.GetRequiredService<IHttpClientFactory>().CreateClient("StarWarsApi"),
                      sp.GetRequiredService<ModelContext>()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
