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
        public List<Speedruns> GetSpeedruns(string shortName, int offset, int len)
        {
            return _context.speedruns.OrderBy(x => x.time).Where(x => x.game.shortName == shortName).Skip(offset).Take(len).ToList();
        }
        public bool AddSpeedrun(string username, string shortName, string country, TimeSpan time, DateTime date, string platform, string category)
        {
            bool speedrunExistsAndIsFaster = _context.speedruns.Any(x => x.username.Equals(username) && x.game.shortName.Equals(shortName) && x.time > time);
            if (speedrunExistsAndIsFaster) return false;
            
            int id = _context.speedruns.Any() ? _context.speedruns.Max(x => x.id) + 1 : 1;
            Speedruns speedruns = new Speedruns(id, _context.games.First(x => x.shortName == shortName).id, username, country, time, date, platform, category);
            _context.speedruns.Add(speedruns);
            _context.SaveChanges();
            return true;
        }
        public bool RemoveSpeedrun(int id)
        {
            bool exists = _context.speedruns.Any(x => x.id == id);
            if (!exists) return false;

            _context.speedruns.Remove(_context.speedruns.First(x => x.id == id));
            _context.SaveChanges();
            return true;
        }
    }
}
