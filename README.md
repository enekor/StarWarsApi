# StarWarsApi y SWConsoleApp

## Descripción
Este proyecto contiene una API REST (StarWarsApi) y una aplicación de consola (SWConsoleApp) para interactuar con datos del universo Star Wars. La API actúa como wrapper de [SWAPI](https://swapi.dev/) y permite almacenar información en una base de datos SQLite local. La consola permite consultar y guardar películas usando la API.

---

## Pasos para ejecutar

1. **Ejecuta primero la API**
   - Ve a la carpeta `StarWarsApi` y ejecuta:
   ```powershell
   dotnet run
   ```
   La API estará disponible en:
   - https://localhost:7079 (HTTPS)
   - http://localhost:5212 (HTTP)

2. **Ejecuta la aplicación de consola**
   - Ve a la carpeta `SWConsoleApp` y ejecuta:
   ```powershell
   dotnet run
   ```

---

## Endpoints principales

Los endpoints siguen el patrón:
```
/{ControllerName}/{Action}
```
Donde `{ControllerName}` es el nombre del controller sin la palabra `Controller` (por ejemplo, `Film` para `FilmController`).

### Importante sobre `/Film/GetFilms`
- El endpoint `GET /Film/GetFilms` es el root de toda la información y el más completo.
- Al llamarlo, se carga toda la información de películas junto con todos sus personajes, vehículos, planetas, especies y starships relacionados.
- Esto permite una navegación fluida y sin esperas en la aplicación de consola, ya que todos los datos necesarios se obtienen de una sola vez.
- **Nota:** Debido a la cantidad de datos que devuelve, la primera carga puede tardar unos segundos.

Ejemplos para películas:
- `GET    /Film/GetFilms`              (películas desde SWAPI, con toda la información relacionada)
- `GET    /Film/GetFilmsFromDatabase`  (películas desde la base de datos local)
- `POST   /Film/SaveFilm`              (guardar película en la base de datos)
- `DELETE /Film/DeleteFilm/{id}`       (eliminar película de la base de datos)

Otros controllers disponibles: `Character`, `Planet`, `Specie`, `Starship`, `Vehicle`.

Consulta las anotaciones `[HttpGet]`, `[HttpPost]`, `[HttpDelete]` en los controllers para ver todos los endpoints.

---

## Esquema de la base de datos (SQLite)

A continuación se muestran las sentencias SQL para crear las tablas principales:

```sql
CREATE TABLE "characters" (
    "_id" TEXT PRIMARY KEY,
    "description" TEXT,
    "created" TEXT NOT NULL,
    "edited" TEXT NOT NULL,
    "name" TEXT,
    "gender" TEXT,
    "skin_color" TEXT,
    "hair_color" TEXT,
    "height" TEXT,
    "eye_color" TEXT,
    "mass" TEXT,
    "homeworld" TEXT,
    "birth_year" TEXT,
    "url" TEXT
);

CREATE TABLE "films" (
    "_id" TEXT PRIMARY KEY,
    "description" TEXT,
    "created" TEXT NOT NULL,
    "edited" TEXT NOT NULL,
    "starships" TEXT,
    "vehicles" TEXT,
    "planets" TEXT,
    "producer" TEXT,
    "title" TEXT,
    "episode_id" INTEGER NOT NULL,
    "director" TEXT,
    "release_date" TEXT,
    "opening_crawl" TEXT,
    "characters" TEXT,
    "species" TEXT,
    "url" TEXT
);

CREATE TABLE "planets" (
    "_id" TEXT PRIMARY KEY,
    "description" TEXT,
    "created" TEXT NOT NULL,
    "edited" TEXT NOT NULL,
    "climate" TEXT,
    "surface_water" TEXT,
    "name" TEXT,
    "diameter" TEXT,
    "rotation_period" TEXT,
    "terrain" TEXT,
    "gravity" TEXT,
    "orbital_period" TEXT,
    "population" TEXT,
    "url" TEXT
);

CREATE TABLE "species" (
    "_id" TEXT PRIMARY KEY,
    "description" TEXT,
    "created" TEXT NOT NULL,
    "edited" TEXT NOT NULL,
    "classification" TEXT,
    "name" TEXT,
    "designation" TEXT,
    "language" TEXT,
    "homeworld" TEXT,
    "average_lifespan" TEXT,
    "average_height" TEXT,
    "url" TEXT
);

CREATE TABLE "starships" (
    "_id" TEXT PRIMARY KEY,
    "description" TEXT,
    "created" TEXT NOT NULL,
    "edited" TEXT NOT NULL,
    "consumables" TEXT,
    "name" TEXT,
    "cargo_capacity" TEXT,
    "passengers" TEXT,
    "max_atmosphering_speed" TEXT,
    "crew" TEXT,
    "length" TEXT,
    "model" TEXT,
    "cost_in_credits" TEXT,
    "manufacturer" TEXT,
    "mglt" TEXT,
    "starship_class" TEXT,
    "hyperdrive_rating" TEXT,
    "url" TEXT
);

CREATE TABLE "vehicles" (
    "_id" TEXT PRIMARY KEY,
    "description" TEXT,
    "created" TEXT NOT NULL,
    "edited" TEXT NOT NULL,
    "consumables" TEXT,
    "name" TEXT,
    "cargo_capacity" TEXT,
    "passengers" TEXT,
    "max_atmosphering_speed" TEXT,
    "crew" TEXT,
    "length" TEXT,
    "model" TEXT,
    "cost_in_credits" TEXT,
    "manufacturer" TEXT,
    "vehicle_class" TEXT,
    "url" TEXT
);
```

---

## Testing

Para ejecutar los tests (ubicados en el proyecto `StarWarsApi.Tests`):

```powershell
dotnet test StarWarsApi.Tests/StarWarsApi.Tests.csproj
```

---

## Notas
- La base de datos utilizada es SQLite y se crea automáticamente con las migraciones de Entity Framework.
- Puedes modificar la cadena de conexión en `StarWarsApi/appsettings.json`.
- Para más detalles sobre los endpoints, revisa los controllers y sus anotaciones en la carpeta `StarWarsApi/Controllers`.
- Al iniciar la API, se abre automáticamente una interfaz Swagger en tu navegador donde puedes explorar y probar todos los endpoints de forma interactiva.
