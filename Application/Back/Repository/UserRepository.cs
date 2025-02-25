using Microsoft.EntityFrameworkCore;
using Models;
using Models.Data;
using Repository.Interfaces;

namespace Repository
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        public async Task<TblUsuario?> GetUserByUsername(string username)
        {
            var responseDb = await context.TblUsuarios
                .Where(u => u.UsAlias == username)
                .FirstOrDefaultAsync();

            return responseDb;
        }
    }
}
