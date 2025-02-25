using Microsoft.EntityFrameworkCore;
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
    public class BannerRepository(AppDbContext context) : IBannerRepository
    {
        public async Task<TblBanner?> GetByIdAsync(int id)
        {
            var responseDb = await context.TblBanners
                .Where(b => b.BnrEstado == true)
                .FirstOrDefaultAsync();

            return responseDb;
        }

        public async Task AddAsync(TblBanner banner)
        {
            await context.TblBanners.AddAsync(banner);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TblBanner banner)
        {
            var register = await context.TblBanners
                .FindAsync(banner.BnrIdBannerPk);
            if (register == null) return false;

            register.BnrRutaAcceso = banner.BnrRutaAcceso;
            register.BnrTexto = banner.BnrTexto;
            register.BnrEstado = banner.BnrEstado;

            var rowAffected = await context.SaveChangesAsync();
            return rowAffected > 0;
        }

        public async Task<bool> DeleteAsync(int idContent)
        {
            var register = await context.TblBanners.FirstOrDefaultAsync(b => b.BnrIdContenidoFk == idContent);
            if (register == null) return false;

            register.BnrEstado = false;

            var rowAffected = await context.SaveChangesAsync();
            return rowAffected > 0;
        }
    }
}
