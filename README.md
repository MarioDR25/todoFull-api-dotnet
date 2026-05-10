# TodoList API 

Una API robusta y escalable construida con **ASP.NET Core 9**, siguiendo los principios de **Arquitectura Limpia (Clean Architecture)** y utilizando **MySQL** como motor de base de datos.

## 🚀 Tecnologías utilizadas

*   **Runtime:** .NET 9
*   **Base de Datos:** MySQL
*   **ORM:** Entity Framework Core
*   **Documentación:** OpenAPI / Scalar 
*   **Patrones:** Repository Pattern, Dependency Injection, DTOs.

## 🏗️ Estructura del Proyecto

El proyecto está dividido en capas para asegurar el desacoplamiento:
*   **Domain:** Entidades de negocio e interfaces de repositorio.
*   **Application:** Servicios, lógica de negocio y DTOs.
*   **Infrastructure:** Implementación de repositorios, DbContext y configuraciones de MySQL.
*   **Api:** Controladores y configuración del punto de entrada (Program.cs).

## 🛠️ Configuración Local

### 1. Prerrequisitos
*   [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
*   [MySQL Server](https://dev.mysql.com/downloads/installer/)

### 2. Base de Datos
Clona el repositorio y configura tu cadena de conexión en el archivo `appsettings.json` 
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=TodoListDb;Uid=tu_usuario;Pwd=tu_contraseña;"
```
### 3. Ejecutar Migraciones
Para crear las tablas en tu base de datos local, ejecuta el siguiente comando desde la raíz:
```bash
dotnet ef database update --project src/Infrastructure --startup-project ..\TodoList.Api\
```
### 4. Iniciar la API
```bash
dotnet run --project src/TodoList.Api
```
## 📖 Documentación con Scalar
Esta API utiliza Scalar para una experiencia de documentación interactiva y moderna. Una vez que la aplicación esté corriendo, puedes acceder a la interfaz de pruebas en:

🔗 URL: http://localhost:XXXX/scalar/v1
(Reemplaza XXXX por el puerto que asigne .NET, usualmente 5000 o 7000)

Características de Scalar en este proyecto:
Pruebas en vivo: Prueba cada endpoint directamente desde el navegador.

Descarga de Contratos: Puedes descargar el archivo openapi.json desde la interfaz.

Snippets de código: Genera automáticamente el código para consumir la API en múltiples lenguajes (JavaScript, Python, etc.).
