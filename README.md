# StarWarsApi

Esta es una API REST que sirve como wrapper para la [SWAPI (Star Wars API)](https://swapi.dev/), proporcionando endpoints para acceder a información sobre personajes, películas, planetas, especies, naves espaciales y vehículos del universo de Star Wars.

## Características

- Integración con SWAPI
- Almacenamiento en base de datos local
- Mapeo entre diferentes modelos de datos (API, Database, Controller)
- Endpoints RESTful para todas las entidades
- Tests unitarios y de integración

## Tecnologías Utilizadas

- .NET 8.0
- Entity Framework Core
- NUnit (para testing)
- Moq (para mocking en tests)

## Estructura del Proyecto

```
StarWarsApi/
├── Controllers/          # Controladores REST
├── Services/            # Lógica de negocio
├── Models/              # Modelos de datos
│   ├── api/            # Modelos de SWAPI
│   ├── controller/     # DTOs
│   └── database/       # Entidades de base de datos
├── Mappers/            # Conversión entre modelos
└── Tests/              # Tests unitarios y de integración
```

## Requisitos Previos

- .NET 8.0 SDK
- Visual Studio 2022 o Visual Studio Code
- SQL Server (opcional, por defecto usa SQLite)

## Instalación

1. Clonar el repositorio:
```powershell
git clone [url-del-repositorio]
cd StarWarsApi
```

2. Restaurar las dependencias:
```powershell
dotnet restore
```

3. Actualizar la base de datos:
```powershell
dotnet ef database update
```

## Configuración

La configuración de la aplicación se encuentra en `appsettings.json`. Puedes modificar:

- Conexión a la base de datos
- URL base de SWAPI
- Otros parámetros de configuración

## Ejecución

1. Para ejecutar la aplicación:
```powershell
dotnet run
```

2. La API estará disponible en:
- https://localhost:7272 (HTTPS)
- http://localhost:5272 (HTTP)

## Endpoints Disponibles

- `GET /api/character/GetCharacters` - Obtener todos los personajes desde SWAPI
- `POST /api/character/SaveCharacter` - Añadir un personaje de la api a la base de datos
- `DELETE /api/character/deleteCharacter/{id}` - Eliminar un personaje de la base de datos local

Similar para:
- `/api/film`
- `/api/planet`
- `/api/specie`
- `/api/starship`
- `/api/vehicle`

## Testing

Para ejecutar los tests:
```powershell
dotnet test
```

Los tests incluyen:
- Tests unitarios con mocking
- Tests de integración con la API real
- Tests de la capa de persistencia
