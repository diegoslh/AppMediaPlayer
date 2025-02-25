using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public class StorageService : IStorageService
    {
        private readonly AppDbContext _context;
        private readonly string _baseRoute;
        public StorageService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _baseRoute = Path.Combine(env.ContentRootPath, "ContentUploaded");
        }

        public async Task<string> SaveFile(string folderName, IFormFile file)
        {
            // Verificación de Carpeta
            var folderRoute = Path.Combine(_baseRoute, folderName);
            if (!Directory.Exists(folderRoute))
            {
                Directory.CreateDirectory(folderRoute);
            }

            // Generar la ruta completa donde se guardará el archivo
            var fileExtension = Path.GetExtension(file.FileName); //extensión del archivo original
            var filename = $"{folderName.ToUpperInvariant()}_{DateTime.Now:yyyyMMdd_HHmmss}{fileExtension}";
            var fileRoute = Path.Combine(folderRoute, filename);

            try
            {
                // Guardar el archivo de manera asincrónica
                await using (var stream = new FileStream(fileRoute, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Retornar la ruta del archivo guardado
                var relativeRoute = Path.GetRelativePath(_baseRoute, fileRoute);
                return relativeRoute;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Ups! Hubo un error al guardar el archivo.", e);
            }
        }

        public async Task<bool> GuardarRutaDb(int idSolicitud, string rutaRelativa)
        {
            try
            {
                //Buscar registro
                //var registroExistente = await _context
                //    .LvcTblNovedadesLaborales
                //    .Where(n => n.NovIdNovedadPk == idSolicitud)
                //    .FirstOrDefaultAsync();

                //if (registroExistente == null) return false;

                ////Actualizar ruta
                //registroExistente.NovRutaAdjunto = rutaRelativa;
                //await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<(byte[]? archivoBytes, string nombreArchivo, string? errorMessage)> ObtenerArchivo(string rutaArchivoDb)
        {
            try
            {
                //Ruta archivo
                var rutaCompleta = Path.Combine(_baseRoute, rutaArchivoDb);

                //Verificación de existencia de ruta
                if (!System.IO.File.Exists(rutaCompleta))
                {
                    return (null, null, "El archivo no existe.")!;
                }

                //Leer bytes de archivo
                var archivoBytes = await System.IO.File.ReadAllBytesAsync(rutaCompleta);

                return (archivoBytes, rutaArchivoDb, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (null, null, "Hubo un error al procesar la solicitud.")!;
            }
        }
    }
}
