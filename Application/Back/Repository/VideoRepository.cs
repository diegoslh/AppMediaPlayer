using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models;
using Models.Data;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class VideoRepository(AppDbContext context) : IVideoRepository
    {
        public async Task<TblVideo?> GetByIdAsync(int id)
        {
            var responseDb = await context.TblVideos
                .Where(b => b.VdoEstado == true)
                .FirstOrDefaultAsync();

            return responseDb;
        }

        public async Task AddAsync(TblVideo video)
        {
            await context.TblVideos.AddAsync(video);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TblVideo video)
        {
            var register = await context.TblVideos
               .FindAsync(video.VdoIdVideoPk);
            if (register == null) return false;

            register.VdoRutaAcceso = video.VdoRutaAcceso;
            register.VdoEstado = video.VdoEstado;

            var rowAffected = await context.SaveChangesAsync();
            return rowAffected > 0;
        }

        public async Task<bool> DeleteAsync(int idContent)
        {
            var register = await context.TblVideos
               .FirstOrDefaultAsync(v => v.VdoIdContenidoFk == idContent);
            if (register == null) return false;

            register.VdoEstado = false;

            var rowAffected = await context.SaveChangesAsync();
            return rowAffected > 0;
        }
    }
}
