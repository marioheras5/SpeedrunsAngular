using Microsoft.AspNetCore.Mvc;
using SpeedrunsAngular.Actions;
using SpeedrunsAngular.Data;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace SpeedrunsAngular.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private SpeedrunsDbContext _context;
        public LoginController(SpeedrunsDbContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Comprueba si el usuario esta registrado y si la contraseña es correcta
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login([FromForm] string username, [FromForm] string password)
        {
            LoginActions loginActions = new LoginActions(_context);
            return loginActions.Login(username, password);
        }
        
        /// <summary>
        /// Registra un usuario
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("signup")]
        public HttpResponseMessage Register([FromForm] string username, [FromForm] string password)
        {
            LoginActions loginActions = new LoginActions(_context);
            return loginActions.Register(username, password);
        }
    }
}