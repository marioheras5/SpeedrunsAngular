using Microsoft.AspNetCore.Mvc;
using SpeedrunsAngular.Actions;
using SpeedrunsAngular.Data;
using SpeedrunsAngular.Models;
using System.Runtime.InteropServices;

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
        public List<SpeedrunModel> GetSpeedruns(string shortName, string category, int offset, int len, string search = "")
        {
            SpeedrunActions speedrunActions = new SpeedrunActions(_context);
            return speedrunActions.GetSpeedruns(shortName, category, offset, len, search);
        }
        /// <summary>
        /// Añade una speedrun.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="shortName"></param>
        /// <param name="img"></param>
        [HttpPost]
        [Route("AddSpeedrun")]
        public bool AddSpeedrun([FromForm] string username, [FromForm] string shortName, [FromForm] string country, [FromForm] string time, [FromForm] DateTime date, [FromForm] string platform, [FromForm] string category)
        {
            SpeedrunActions speedrunActions = new SpeedrunActions(_context);

            string[] timeSplit = time.Split(':');
            int hours = int.Parse(timeSplit[0]);
            int minutes = int.Parse(timeSplit[1]);
            int seconds = int.Parse(timeSplit[2]);
            return speedrunActions.AddSpeedrun(username, shortName, country, new TimeSpan(hours, minutes, seconds), date, platform, category);
        }
        /// <summary>
        /// Obtiene las categorias
        /// </summary>
        /// <param name="name"></param>
        /// <param name="shortName"></param>
        /// <param name="img"></param>
        [HttpGet]
        [Route("GetCategories")]
        public List<string> GetCategories(string shortName)
        {
            int id = _context.games.Where(x => x.shortName == shortName).Select(x => x.id).FirstOrDefault();
            return _context.speedrun.Where(x => x.id_game == id).OrderBy(x => x.category).Select(x => x.category).ToHashSet().ToList();
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
