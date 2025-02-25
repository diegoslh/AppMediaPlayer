using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.Interfaces;

namespace API.Controllers
{
    [ApiController]
    //[ValidateAntiForgeryToken]
    [Route("api/[controller]")]
    public class ContentController(IContentService contentService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var enviorementRoute = $"{Request.Scheme}://{Request.Host}";
                var response = await contentService.GetAllContent(enviorementRoute);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Ups! Ha ocurrido un error inesperado: {ex.Message}" });
            }
        }

        [Authorize]
        [HttpGet("types")]
        public async Task<ActionResult> GetContentTypes()
        {
            try
            {
                var response = await contentService.GetContentTypes();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Ups! Ha ocurrido un error inesperado: {ex.Message}" });
            }
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult> Create([FromForm] ContentDto form)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else if (form.CtoBanner == null && form.CtoVideo == null)
                {
                    return BadRequest(new { success = false, error = "Ups! No se han encontrado archivos multimedia para guardar." });
                }

                await contentService.AddContent(form);
                return Ok(new { success = true, message = "Contenido almacenado exitosamente!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Ups! Ha ocurrido un error inesperado: {ex.Message}" });
            }
        }

        // POST: ContentController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //[Authorize]
        [HttpPut("delete")]
        public async Task<ActionResult> Delete([FromQuery] int id, [FromQuery] int typeContent)
        {
            try
            {
                await contentService.DeleteContent(id, typeContent);
                return Ok(new { success = true, message = "Contenido eliminado!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = $"Ups! Ha ocurrido un error inesperado: {ex.Message}" });
            }
        }


        [HttpGet("programation")]
        public async Task<ActionResult> GetProgramationHours()
        {
            try
            {
                var response = await contentService.GetProgramationContent();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Ups! Ha ocurrido un error inesperado: {ex.Message}" });
            }
        }

        [Authorize]
        [HttpPost("setcontent")]
        public async Task<ActionResult> SetHourContent([FromQuery] int id, [FromBody] SetHourDto form)
        {
            try
            {
                await contentService.SetContentHour(id, form.Hour);
                return Ok(new { success = true, message = "Hora de contenido programada!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = $"Ups! Ha ocurrido un error inesperado: {ex.Message}" });
            }
        }
    }
}
