using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IStorageService
    {
        Task<string> SaveFile(string folderName, IFormFile file);
        Task<bool> GuardarRutaDb(int idSolicitud, string rutaRelativa);
        Task<(byte[]? archivoBytes, string nombreArchivo, string? errorMessage)> ObtenerArchivo(string rutaArchivoDb);
    }
}
