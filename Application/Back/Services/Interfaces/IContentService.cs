using Models;
using Models.Shared;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IContentService
    {
        Task<List<SpContentDto>> GetAllContent(string enviorementRoute);
        //Task<TblContenido?> GetByIdAsync(int id);
        Task<List<ContentTypesDto>> GetContentTypes();
        Task AddContent(ContentDto content);
        Task<bool> UpdateContent(ContentDto content);
        Task<bool> DeleteContent(int id, int typeContent);
        Task SetContentHour(int id, TimeOnly hour);
        Task<List<TblProgramacionContenido>> GetProgramationContent();
    }
}
