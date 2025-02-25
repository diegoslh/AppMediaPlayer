using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IBannerRepository
    {
        //Task<List<TblBanner>> GetAllAsync();
        Task<TblBanner?> GetByIdAsync(int id);
        Task AddAsync(TblBanner product);
        Task<bool> UpdateAsync(TblBanner product);
        Task<bool> DeleteAsync(int idContent);
    }
}
