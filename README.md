# Oniria Solution

Oniria es una solución integral desarrollada en **.NET 7** implementando los principios de **Clean Architecture**. El sistema está diseñado para la gestión de pacientes, análisis de sueños, membresías y recursos humanos.

## 🏗 Arquitectura

El proyecto sigue una estructura de Clean Architecture separada en múltiples capas para asegurar la escalabilidad y mantenibilidad:

- **Oniria (Frontend):** Aplicación Web MVC que sirve como interfaz de usuario.
- **OniriaApi (Backend):** API RESTful que expone la lógica de negocio.
- **Oniria.Core:**
  - **Domain:** Contiene las entidades del negocio y la lógica central (Dreams, Patients, Memberships).
  - **Application:** Define interfaces, DTOs y la lógica de aplicación agnóstica de la infraestructura.
- **Oniria.Infrastructure:**
  - **Persistence:** Implementación de acceso a datos con Entity Framework Core y SQL Server.
  - **Identity:** Gestión de autenticación y autorización.
  - **Shared:** Servicios compartidos (Emails, utilidades).

## 🚀 Tecnologías

- **Framework:** .NET 7.0
- **Base de Datos:** SQL Server (Express por defecto)
- **ORM:** Entity Framework Core
- **Docs API:** Swagger / OpenAPI
- **Frontend:** ASP.NET Core MVC con Razor Views

## 📋 Módulos Principales

El dominio de la aplicación abarca las siguientes áreas:

1.  **Gestión de Sueños (Dreams):** Registro y análisis de sueños (`Dream`, `DreamAnalysis`, `DreamToken`).
2.  **Pacientes (Patients):** Gestión de información de pacientes y sus estados emocionales.
3.  **Membresías (Memberships):** Administración de beneficios, categorías y adquisiciones de membresías.
4.  **Recursos Humanos:** Gestión de empleados (`Employee`) y organización.

## 🛠 Configuración y Ejecución

### Requisitos Previos
- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- SQL Server LocalDB o SQL Express.

### Pasos para Ejecutar

#### 1. Base de Datos
Asegúrese de que la cadena de conexión en `appsettings.json` apunte a su instancia de SQL Server. Luego, aplique las migraciones:

```powershell
# Desde la carpeta raíz (donde está la solución o el subdirectorio de infraestructura)
dotnet ef database update --project ..\Oniria.Infrastructure.Persistence --startup-project ..\OniriaApi
```
*(Nota: Ajuste las rutas relativas según desde donde ejecute el comando)*

#### 2. Ejecutar Backend (API)
```powershell
dotnet run --project OniriaApi/OniriaApi.csproj
```
La API estará disponible (por defecto) en `https://localhost:5001` (o puerto configurado). Puede ver la documentación en `/swagger`.

#### 3. Ejecutar Frontend (MVC)
En una nueva terminal:
```powershell
dotnet run --project Oniria/Oniria.csproj
```
La aplicación web estará disponible en el puerto indicado (ej. `https://localhost:7198`).

## 📁 Estructura de Carpetas

```text
/
├── Oniria/                      # Proyecto Frontend (MVC)
├── OniriaApi/                   # Proyecto Backend (Web API)
├── Oniria.Core.Domain/          # Entidades y Reglas de Negocio
├── Oniria.Core.Application/     # Casos de uso e Interfaces
├── Oniria.Core.Dtos/            # Objetos de Transferencia de Datos
├── Oniria.Infrastructure.*/     # Implementación de Infraestructura (DB, Identity, etc.)
└── Oniria.sln                   # Archivo de Solución
```