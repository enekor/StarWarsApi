# SWConsoleApp

Esta aplicación de consola permite interactuar con la API StarWarsApi para consultar y guardar películas del universo Star Wars.

## Requisitos previos
- Tener la API (`StarWarsApi`) ejecutándose primero (ver instrucciones en el README principal).
- .NET 6 o superior

## Uso
1. Ejecuta la API:
   ```powershell
   cd ../StarWarsApi
   dotnet run
   ```
   La API debe estar disponible en:
   - https://localhost:7079 (HTTPS)
   - http://localhost:5212 (HTTP)

2. Ejecuta la consola:
   ```powershell
   dotnet run
   ```

3. Sigue el menú interactivo para:
   - Consultar películas desde SWAPI o la base de datos local
   - Ver detalles, personajes, vehículos, planetas, especies y starships de cada película
   - Guardar películas en la base de datos local

## Notas
- Los endpoints utilizados por la consola están documentados en el README principal del proyecto.
- La base de datos es SQLite y se crea automáticamente al ejecutar la API.
- Puedes modificar la URL base de la API en el código fuente si cambias los puertos.
