# PruebaTecnica API

API REST para gestión de productos desarrollada con .NET 8.

## Tecnologías

- .NET 8
- Entity Framework Core
- MediatR
- FluentValidation
- SQL Server

## Arquitectura

El proyecto implementa **Clean Architecture** combinada con el patrón **CQRS**.

## Estructura del Proyecto

```
BACKEND/
├── PruebaTecnica.Api/           # Capa de presentación
│   ├── Controllers/             # Endpoints REST
│   └── Program.cs               # Configuración de la app
│
├── PruebaTecnica.Application/   # Capa de aplicación
│   ├── Commands/                # Operaciones de escritura (CQRS)
│   │   └── Productos/
│   │       ├── Create/
│   │       ├── Update/
│   │       └── Delete/
│   ├── Queries/                 # Operaciones de lectura (CQRS)
│   │   └── Productos/
│   │       ├── GetAll/
│   │       ├── GetById/
│   │       └── Common/          # DTOs compartidos
│   ├── Behaviours/              # Pipeline behaviors (validaciones)
│   └── IRepositories/           # Interfaces de repositorios
│
├── PruebaTecnica.Core/          # Capa de dominio
│   └── Entities/                # Entidades del negocio
│
├── PruebaTecnica.Infrastructure/# Capa de infraestructura
│   ├── Repositories/            # Implementación de repositorios
│   ├── Configuration/           # Configuración EF Core
│   └── Migrations/              # Migraciones de BD
│
└── Common/                      # Librerías compartidas
    ├── PruebaTecnica.Common.Core/
    └── PruebaTecnica.Common.Application/
```

## CQRS con MediatR

El patrón CQRS separa las operaciones de lectura y escritura:

**Commands** (Escritura):
- `Create/CreateProductoCommand` - Crear producto
- `Update/UpdateProductoCommand` - Actualizar producto
- `Delete/DeleteProductoCommand` - Eliminar producto

**Queries** (Lectura):
- `GetAll/ProductoQuery` - Listar productos con paginación
- `GetById/GetProductoByIdQuery` - Obtener producto por ID

## Endpoints

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/productos` | Listar productos (paginado) |
| GET | `/api/productos/{id}` | Obtener producto por ID |
| POST | `/api/productos` | Crear producto |
| PUT | `/api/productos` | Actualizar producto |
| DELETE | `/api/productos/{id}` | Eliminar producto |

## Configuración

1. Ejecutar la API:

```bash
cd PruebaTecnica.Api
dotnet run
```

La API estará disponible en `https://localhost:5001` con Swagger en `/swagger`.

 