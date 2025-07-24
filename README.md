# AppMediaPlayer
Reproductor de videos automatico con dashboard de administración para programar contenido a horas especificas y administrar contenido.

# Aplicación Cliente-Servidor con React y .NET Core

## 🛠 Tecnologías utilizadas

### 📌 Frontend
- [React](https://react.dev/)
- Vite

### 🔗 Backend
- .NET Core 8 (ASP.NET Web API)
- Entity Framework Core para el ORM
- JWT Authentication (para autenticación)

### 🗄️ Base de Datos
- SQL Server

## 🚀 Instalación y configuración

### ✅ Requisitos previos

Antes de comenzar, asegúrate de tener configurado en tu sistema:

* **Docker** y **Docker Compose** correctamente instalados y funcionando.
* Dependiendo de tu sistema operativo, puede que debas configurar herramientas complementarias necesarias para el uso de Docker.

---

### ⚙️ Levantar la aplicación con Docker

1. Clona el repositorio:

   ```bash
   git clone https://github.com/diegoslh/AppMediaPlayer.git
   cd AppMediaPlayer
   ```

2. Levanta toda la aplicación con Docker Compose:

   ```bash
   docker compose up --build
   ```

   Este comando construirá las imágenes necesarias y ejecutará todos los servicios:

   * **Frontend (React)** en [http://localhost:3000](http://localhost:3000)
   * **Backend (API .NET Core)** en [http://localhost:5000](http://localhost:5000)
   * **Base de datos SQL Server** expuesta en el puerto `1435`

3. Un **usuario inicial** será creado automáticamente como parte de la migración, permitiéndote iniciar sesión y comenzar a usar la plataforma de inmediato.
   - Username: Admin23
   - Password: 123

---

### 🧱 Estructura de servicios (Docker Compose)

| Servicio   | Descripción                       | Puerto local (Host)    |
| ---------- | --------------------------------- | ---------------------- |
| `frontend` | Aplicación React (UI cliente)     | `3000`                 |
| `api`      | API REST en .NET Core             | `5000`                 |
| `db`       | SQL Server                        | `1435` |
| `migrator` | Ejecuta las migraciones iniciales | N/A (no expone puerto) |

---

Una vez todos los contenedores estén en ejecución, puedes acceder a la interfaz en:

🔗 [http://localhost:3000](http://localhost:3000/)

El backend expone sus endpoints en:

🔗 [http://localhost:5000/api](http://localhost:5000/api/)

> 💡 Nota: El backend no tiene interfaz visual, solo endpoints que se consumen desde el frontend.

---
