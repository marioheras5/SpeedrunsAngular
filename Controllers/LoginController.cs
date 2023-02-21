using Microsoft.AspNetCore.Mvc;

namespace SpeedrunsAngular.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public bool Login(string username, string password)
        {
            // Busco en base de datos este username y esta password
            
            return username == "admin" && password == "admin";
        }
        [HttpPost]
        public bool Register(string username, string password)
        {
            // Si el usuario no esta registrado, lo añadimos

            return username == "admin" && password == "admin";
        }
    }
}