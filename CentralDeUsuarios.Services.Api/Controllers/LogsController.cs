using CentralDeUsuarios.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CentralDeUsuarios.Services.Api.Controllers
{
    [Authorize(Roles = "USER")]
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly IUsuarioAppService _usuarioAppService;

        public LogsController(IUsuarioAppService usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var emailUsuario = User.Identity.Name;
            var logs = _usuarioAppService.ConsultarLogDeUsuario(emailUsuario);

            return StatusCode(200, logs);
        }
    }
}
