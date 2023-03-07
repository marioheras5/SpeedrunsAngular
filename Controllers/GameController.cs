using Microsoft.AspNetCore.Mvc;
using SpeedrunsAngular.Actions;
using SpeedrunsAngular.Data;
using SpeedrunsAngular.Models;

namespace SpeedrunsAngular.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private SpeedrunsDbContext _context;
        public GameController(SpeedrunsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene la lista de juegos, con la paginación indicada
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetGames")]
        public List<Games> GetGames(int offset, int len)
        {
            GameActions gameActions = new GameActions(_context);
            return gameActions.GetGames(offset, len);
        }
        /// <summary>
        /// Obtiene un juego
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetGame")]
        public Games GetGame(string shortName)
        {
            GameActions gameActions = new GameActions(_context);
            return gameActions.GetGame(shortName);
        }
        /// <summary>
        /// Añade un juego a partir de un nombre, su abreviación y una imagen.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="shortName"></param>
        /// <param name="img"></param>
        [HttpPost]
        [Route("AddGame")]
        public bool AddGame([FromForm] string name, [FromForm] string shortName, [FromForm] IFormFile img)
        {
            GameActions gameActions = new GameActions(_context);
            return gameActions.AddGame(name, shortName, img);
        }
        /// <summary>
        /// Elimina un juego a partir de un id.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="shortName"></param>
        /// <param name="img"></param>
        [HttpDelete]
        [Route("RemoveGame")]
        public bool RemoveGame([FromForm] int id)
        {
            GameActions gameActions = new GameActions(_context);
            return gameActions.RemoveGame(id);
        }
    }
}
