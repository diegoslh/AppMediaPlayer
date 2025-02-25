using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.Interfaces;

namespace API.Controllers
{
    [ApiController]
    //[AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthenticationController(IAuthenticationUserService authenticationUser) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto form)
        {
            try
            {
                var responseDb = await authenticationUser.AuthenticateUser(form.Username, form.Password);
                return Ok(responseDb);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, "Ups! Ha ocurrido un error: " + ex.Message);
            }
        }
    }
}
