## 🧱 Comandos para Ejecutar Migraciones

Para este proyecto estructurado en capas, donde los modelos (`DbContext` y entidades) están en un proyecto separado del que contiene la configuración de la aplicación (`Program.cs` y `appsettings.json`), es necesario **especificar explícitamente** el proyecto de inicio (`StartupProject`) y el proyecto donde se encuentran los modelos (`Project`) al ejecutar comandos de migración.

---

### ✅ Usando la Consola de NuGet (Visual Studio)

```powershell
# 1. Crear una migración
Add-Migration NombreDeLaMigracion -StartupProject "API" -Project "Models"

# 2. Aplicar la migración a la base de datos
Update-Database -StartupProject "API" -Project "Models"
```

---

### ✅ Usando PowerShell o la Terminal con la CLI de .NET

```powershell
# 1. Crear una migración
dotnet ef migrations add NombreDeLaMigracion --project ./Models --startup-project ./API

# 2. Aplicar la migración a la base de datos
dotnet ef database update --project ./Models --startup-project ./API
```

---

### 📌 Notas

* Asegúrate de ejecutar los comandos desde la raíz de la solución o ajustar las rutas relativas según tu estructura de carpetas.
* Requiere tener instalado el paquete de herramientas EF Core CLI. Puedes instalarlo con:

```powershell
dotnet tool install --global dotnet-ef
```

* Para verificar la instalación:

```powershell
dotnet ef --version
```

---
