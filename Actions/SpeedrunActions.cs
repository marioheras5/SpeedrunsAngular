using Microsoft.AspNetCore.Mvc;
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
        public List<SpeedrunModel> GetSpeedruns(string shortName, string category, int offset, int len, string search)
        {
            List<Speedrun> lista;
            if (search != "") // con busqueda de usuario
            {
                lista = _context.speedrun.OrderBy(x => x.time).Where(x => x.game.shortName == shortName && x.category == category && x.username.StartsWith(search)).Skip(offset).Take(len).ToList();
            }
            else
            {
                lista = _context.speedrun.OrderBy(x => x.time).Where(x => x.game.shortName == shortName && x.category == category).Skip(offset).Take(len).ToList();
            }

            List<SpeedrunModel> listaModel = new List<SpeedrunModel>();
            int contador = 1;
            foreach (Speedrun speedrun in lista)
            {
                listaModel.Add(new SpeedrunModel()
                {
                    id = speedrun.id,
                    position = contador,
                    username = speedrun.username,
                    country = speedrun.country,
                    time = speedrun.time.Hours + ":" + speedrun.time.Minutes + ":" + speedrun.time.Seconds,
                    date = speedrun.date.ToShortDateString(),
                    platform = speedrun.platform,
                    category = speedrun.category
                });
                contador++;
            }

            return listaModel;
        }
        public bool AddSpeedrun(string username, string shortName, string country, TimeSpan time, DateTime date, string platform, string category)
        {
            bool speedrunExistsAndIsFaster = _context.speedrun.Any(x => x.username.Equals(username) && x.game.shortName.Equals(shortName) && x.category == category && x.time > time);
            if (speedrunExistsAndIsFaster) return false;

            int id = _context.speedrun.Any() ? _context.speedrun.Max(x => x.id) + 1 : 1;
            Speedrun speedruns = new Speedrun()
            {
                id = id,
                id_game = _context.games.First(x => x.shortName == shortName).id,
                username = username,
                country = country,
                time = time,
                date = date,
                platform = platform,
                category = category
            };
            _context.speedrun.Add(speedruns);
            _context.SaveChanges();
            return true;
        }
        public bool RemoveSpeedrun(int id)
        {
            bool exists = _context.speedrun.Any(x => x.id == id);
            if (!exists) return false;

            _context.speedrun.Remove(_context.speedrun.First(x => x.id == id));
            _context.SaveChanges();
            return true;
        }
    }
}
