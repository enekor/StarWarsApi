# SWConsoleApp

Consola para interactuar con los endpoints de FilmController de StarWarsApi.

## Funcionalidad principal
- Obtener todas las películas desde `/Film/GetFilms` y cargarlas en memoria.
- Mostrar títulos y permitir seleccionar una película.
- Mostrar detalles y opciones: ver personajes, vehículos, planetas, especies, starships, o guardar la película en la base de datos (`/Film/SaveFilm`).
- Si se elige ver una entidad, muestra solo los datos ya cargados en memoria.
- Si se elige guardar, envía el JSON de la película seleccionada por POST.

## Dependencias
- .NET 6+
- Newtonsoft.Json

## Uso
1. Ejecuta la app: `dotnet run`
2. Sigue el menú interactivo.
