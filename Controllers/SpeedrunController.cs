using Microsoft.AspNetCore.Mvc;
using SpeedrunsAngular.Data;

namespace SpeedrunsAngular.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpeedrunController : ControllerBase
    {
        private SpeedrunsDbContext _context;
        public SpeedrunController(SpeedrunsDbContext context)
        {
            _context = context;
        }
        
    }
}
