using SpeedrunsAngular.Data;
using SpeedrunsAngular.Models;
using System.Security.Cryptography;
using System.Text;

namespace SpeedrunsAngular.Actions
{
    public class SpeedrunActions
    {
        private SpeedrunsDbContext _context;
        public SpeedrunActions(SpeedrunsDbContext context)
        {
            _context = context;
        }
    }
}
