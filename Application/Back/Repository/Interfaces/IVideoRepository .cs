using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IVideoRepository
    {
        //Task<List<TblVideo>> GetAllAsync();
        Task<TblVideo?> GetByIdAsync(int id);
        Task AddAsync(TblVideo product);
        Task<bool> UpdateAsync(TblVideo product);
        Task<bool> DeleteAsync(int idContent);
    }
}
