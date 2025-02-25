using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Data;
using Models.Shared;
using Repository.Interfaces;
using Services.DTOs;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services;
public class ContentService(
    IContentRepository contentRepository,
    IBannerRepository bannerRepository,
    IVideoRepository videoRepository,
    IStorageService storageService,
    AppDbContext context
    )
    : IContentService
{
    public async Task<List<ContentTypesDto>> GetContentTypes()
    {
        try
        {
            var repository = await contentRepository.GetAllTypesAsync();
            var filterResponse = repository
                    .Select(t => new ContentTypesDto
                    {
                        Id = t.TctoIdTipoContenidoPk,
                        Description = $"{t.TctoTipoContenido} - {t.TctoDescripcion}"
                    })
                    .ToList();

            return filterResponse;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Ups! Ha ocurrido un error en: {ex.StackTrace}. Error: {ex.Message}");
        }
    }

    public async Task<List<SpContentDto>> GetAllContent(string enviorementRoute)
    {
        try
        {
            var repository = await contentRepository.GetAllAsync();

            // Complementar la ruta de acceso de los archivos multimedia
            foreach (var item in repository)
            {
                if (item.CtoBanner != null)
                    item.CtoBanner = Path.Combine(enviorementRoute, "media", item.CtoBanner).Replace("\\", "/");
                if (item.CtoVideo != null)
                    item.CtoVideo = Path.Combine(enviorementRoute, "media", item.CtoVideo).Replace("\\", "/");
            }
            return repository;

        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Ups! Ha ocurrido un error en: {ex.StackTrace}. Error: {ex.Message}");
        }
    }

    public async Task AddContent(ContentDto content)
    {
        try
        {
            //Guardar contenido
            TblContenido model = new()
            {
                CtoTitulo = content.CtoTitulo,
                CtoTipoContenidoFk = content.CtoTipoContenidoFk,
                CtoEstado = content.CtoEstado
            };
            await contentRepository.AddAsync(model);

            // Guardar multimedia Banner/Video con id de contenido
            var idContent = model.CtoIdContenidoPk;
            switch (content.CtoTipoContenidoFk)
            {
                //Guardar multimedia Banner
                case 1:
                    await AddBanner(idContent, content.CtoTextoBanner, content.CtoEstado, content.CtoBanner, content.CtoDurationBanner);
                    break;
                //Guardar multimedia Video
                case 2:
                    if (content.CtoVideo == null)
                        throw new ApplicationException("Ups! No se ha encontrado el Video para guardar.");
                    await AddVideo(idContent, content.CtoEstado, content.CtoVideo);

                    break;
                //Guardar multimedia Video y Banner
                case 3:
                    if (content.CtoVideo == null)
                        throw new ApplicationException("Ups! No se ha encontrado el Video para guardar.");

                    await AddVideo(idContent, content.CtoEstado, content.CtoVideo);
                    await AddBanner(idContent, content.CtoTextoBanner, content.CtoEstado, content.CtoBanner, content.CtoDurationBanner);
                    break;
                default:
                    throw new ApplicationException("Ups! No se ha encontrado el tipo de contenido para guardar archivos.");
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Ups! Ha ocurrido un error en: {ex.StackTrace}. Error: {ex.Message}");
        }
    }

    public async Task<bool> UpdateContent(ContentDto content)
    {
        try
        {
            return true;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Ups! Ha ocurrido un error en: {ex.StackTrace}. Error: {ex.Message}");
        }
    }

    public async Task<bool> DeleteContent(int id, int typeContent)
    {
        try
        {
            await contentRepository.DeleteAsync(id);
            switch (typeContent)
            {
                case 1:
                    await bannerRepository.DeleteAsync(id);
                    break;
                case 2:
                    await videoRepository.DeleteAsync(id);
                    break;
                default:
                    await bannerRepository.DeleteAsync(id);
                    await videoRepository.DeleteAsync(id);
                    break;
            }
            return true;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Ups! Ha ocurrido un error en: {ex.StackTrace}. Error: {ex.Message}");
        }
    }
    
    public async Task SetContentHour(int id, TimeOnly hour)
    {
        try
        {
            TblProgramacionContenido setHour = new()
            {
                PcoIdContenidoFk = id,
                PcoHoraProgramada = hour,
                PcoEstado = true
            };
            await context.TblProgramacionContenidos.AddAsync(setHour);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Ups! Ha ocurrido un error en: {ex.StackTrace}. Error: {ex.Message}");
        }
    }

    public async Task<List<TblProgramacionContenido>> GetProgramationContent()
    {
        var responseDb = await context.TblProgramacionContenidos
            .Where(p => p.PcoEstado == true)
            .OrderByDescending(p => p.PcoHoraProgramada)
            .ToListAsync();
        return responseDb;
    }

    #region Private Methods
    private async Task AddBanner(int idContent, string? textoBanner, bool estado, IFormFile? file, int? duration)
    {
        try
        {
            var fileRoute = (file != null) ? await AddFile("Banner", file) : null;
            TblBanner banner = new()
            {
                BnrIdContenidoFk = idContent,
                BnrRutaAcceso = fileRoute,
                BnrTexto = textoBanner ?? null,
                BnrDuracion = duration ?? 10,
                BnrEstado = estado
            };
            await bannerRepository.AddAsync(banner);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Ups! Ha ocurrido un error en: {ex.StackTrace}. Error: {ex.Message}");
        }
    }

    private async Task AddVideo(int idContent, bool estado, IFormFile file)
    {
        try
        {
            var fileRoute = await AddFile("Video", file);
            TblVideo video = new()
            {
                VdoIdContenidoFk = idContent,
                VdoRutaAcceso = fileRoute,
                VdoEstado = estado
            };
            await videoRepository.AddAsync(video);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Ups! Ha ocurrido un error en: {ex.StackTrace}. Error: {ex.Message}");
        }
    }

    private async Task<string> AddFile(string folder, IFormFile file)
    {
        try
        {
            var fileRoute = await storageService.SaveFile(folder, file);
            return fileRoute;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Ups! Ha ocurrido un error en: {ex.StackTrace}. Error: {ex.Message}");
        }
    }

    #endregion
}
