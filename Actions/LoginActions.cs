using SpeedrunsAngular.Data;
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
            string hashedPass = Encoding.Latin1.GetString(sha256.ComputeHash(Encoding.Latin1.GetBytes(password)));

            // Login satisfactorio
            bool success = _context.users.Any(x => x.username == username && password == hashedPass);
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
            string hashedPass = Encoding.Latin1.GetString(sha256.ComputeHash(Encoding.Latin1.GetBytes(password)));

            // Registro al usuario
            _context.users.Add(new Models.Users(username, hashedPass));
            _context.SaveChanges();
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }
}
