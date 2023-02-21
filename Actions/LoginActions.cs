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
        public bool Login(string username, string password)
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
        public bool Register(string username, string password)
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
