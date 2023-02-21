using Microsoft.AspNetCore.Mvc;
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
        public bool Login([FromForm] string username, [FromForm] string password)
        {
            // Busco en base de datos si existe el usuario
            bool userExists = _context.users.Any(x => x.username == username);
            if (!userExists)
            {
                return false;
            }

            // Encripto la contraseña
            using SHA256 sha256 = SHA256.Create();
            string hashedPass = Encoding.Latin1.GetString(sha256.ComputeHash(Encoding.Latin1.GetBytes(password)));

            // Login satisfactorio
            bool success = _context.users.Any(x => x.username == username && password == hashedPass);
            return success;
        }
        
        /// <summary>
        /// Registra un usuario
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Register([FromForm] string username, [FromForm] string password)
        {
            // Busco en base de datos si existe el usuario
            bool userExists = _context.users.Any(x => x.username == username);
            if (userExists)
            {
                return false;
            }

            // Encripto la contraseña
            using SHA256 sha256 = SHA256.Create();
            string hashedPass = Encoding.Latin1.GetString(sha256.ComputeHash(Encoding.Latin1.GetBytes(password)));

            // Registro al usuario
            _context.users.Add(new Models.Users(username, hashedPass));
            _context.SaveChanges();
            return true;
        }
    }
}