using Microsoft.AspNetCore.Mvc;
using SpeedrunsAngular.Actions;
using SpeedrunsAngular.Data;
using SpeedrunsAngular.Models;

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
        /// <summary>
        /// Obtiene la lista de speedruns en base a un juego, con la paginación indicada
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSpeedruns")]
        public List<Speedruns> GetSpeedruns(string shortName, int offset, int len)
        {
            SpeedrunActions speedrunActions = new SpeedrunActions(_context);
            return speedrunActions.GetSpeedruns(shortName, offset, len);
        }
        /// <summary>
        /// Añade una speedrun.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="shortName"></param>
        /// <param name="img"></param>
        [HttpPost]
        [Route("AddSpeedrun")]
        public bool AddSpeedrun([FromForm] string username, [FromForm] string shortName, [FromForm] string country, [FromForm] TimeSpan time, [FromForm] DateTime date, [FromForm] string platform, [FromForm] string category)
        {
            SpeedrunActions speedrunActions = new SpeedrunActions(_context);
            return speedrunActions.AddSpeedrun(username, shortName, country, time, date, platform, category);
        }
        /// <summary>
        /// Elimina una speedrun a partir de un id.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("RemoveSpeedrun")]
        public bool RemoveSpeedrun([FromForm] int id)
        {
            SpeedrunActions speedrunActions = new SpeedrunActions(_context);
            return speedrunActions.RemoveSpeedrun(id);
        }
    }
}
