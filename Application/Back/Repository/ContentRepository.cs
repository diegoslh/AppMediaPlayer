using Microsoft.EntityFrameworkCore;
using Models;
using Models.Data;
using Models.Shared;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ContentRepository(AppDbContext context) : IContentRepository
    {
        public async Task<List<TblDicTipoContenido>> GetAllTypesAsync()
        {

            var responseDb = await context.TblDicTipoContenidos
                .Where(t => t.TctoEstado == true)
                .ToListAsync();

            return responseDb;
        }

        public async Task<List<SpContentDto>> GetAllAsync()
        {

            var responseDb = await context.Set<SpContentDto>()
                .FromSqlRaw("EXECUTE SP_GetAllFullContent")
                .ToListAsync();

            return responseDb;
        }

        public async Task<TblContenido?> GetByIdAsync(int id)
        {
            var responseDb = await context.TblContenidos
                .Where(c => c.CtoEstado == true && c.CtoIdContenidoPk == id)
                .FirstOrDefaultAsync();

            return responseDb;
        }

        public async Task AddAsync(TblContenido content)
        {
            await context.TblContenidos.AddAsync(content);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TblContenido content)
        {
            var register = await context.TblContenidos.FindAsync(content.CtoIdContenidoPk);
            if (register == null) return false;

            register.CtoTitulo = content.CtoTitulo;
            register.CtoTipoContenidoFk = content.CtoTipoContenidoFk;
            register.CtoEstado = content.CtoEstado;

            var rowAffected = await context.SaveChangesAsync();
            return rowAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var register = await context.TblContenidos.FindAsync(id);
            if (register == null) return false;

            register.CtoEstado = false;

            var rowAffected = await context.SaveChangesAsync();
            return rowAffected > 0;
        }
    }
}
