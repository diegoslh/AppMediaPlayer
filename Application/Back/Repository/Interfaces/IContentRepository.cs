using Models;
using Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IContentRepository
    {
        Task<List<TblDicTipoContenido>> GetAllTypesAsync();
        Task<List<SpContentDto>> GetAllAsync();
        Task<TblContenido?> GetByIdAsync(int id);
        Task AddAsync(TblContenido content);
        Task<bool> UpdateAsync(TblContenido content);
        Task<bool> DeleteAsync(int id);
    }
}
