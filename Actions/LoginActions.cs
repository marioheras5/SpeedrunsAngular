using SpeedrunsAngular.Data;
using SpeedrunsAngular.Models;
using System.Security.Cryptography;
using System.Text;

namespace SpeedrunsAngular.Actions
{
    public class LoginActions
    {
        private SpeedrunsDbContext _context;
        public LoginActions(SpeedrunsDbContext context)
        {
            _context = context;
        }
        public HttpResponseMessage Login(string username, string password)
        {
            // Busco en base de datos si existe el usuario
            bool userExists = _context.users.Any(x => x.username == username);
            if (!userExists)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }

            // Encripto la contraseña
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            string hashedPass = sb.ToString();

            // Login satisfactorio
            bool success = _context.users.Any(x => x.username == username && x.password == hashedPass);
            if (!success)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            }
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
        public HttpResponseMessage Register(string username, string password)
        {
            // Busco en base de datos si existe el usuario
            bool userExists = _context.users.Any(x => x.username == username);
            if (userExists)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }

            // Encripto la contraseña
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            string hashedPass = sb.ToString();

            // Registro al usuario
            _context.users.Add(new Models.Users(username, hashedPass));
            _context.SaveChanges();
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }
}
