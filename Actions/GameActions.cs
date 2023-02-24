using Microsoft.AspNetCore.Mvc;
using SpeedrunsAngular.Data;
using SpeedrunsAngular.Models;
using System.Security.Cryptography;
using System.Text;

namespace SpeedrunsAngular.Actions
{
    public class GameActions
    {
        private SpeedrunsDbContext _context;
        public GameActions(SpeedrunsDbContext context)
        {
            _context = context;
        }
        public List<Games> GetGames(int offset, int len)
        {
            return _context.games.Skip(offset).Take(len).ToList();
        }
        public bool AddGame(string name, string shortName, IFormFile img)
        {
            bool alreadyExists = _context.games.Any(x => x.name.Equals(name) || x.shortName.Equals(shortName));
            if (alreadyExists) return false;

            string imgPath = Path.Combine(AppContext.BaseDirectory.Split("bin").First(), "Images", img.FileName);
            using (Stream stream = new FileStream(imgPath, FileMode.Create))
            {
                img.CopyTo(stream);
            }
            int id = _context.games.Max(x => x.id) + 1;
            Games newGame = new Games(id, name, shortName, imgPath);
            _context.games.Add(newGame);
            _context.SaveChanges();
            return true;
        }
        public bool RemoveGame(int gameId)
        {
            bool exists = _context.games.Any(x => x.id == gameId);
            if (!exists) return false;
            
            _context.games.Remove(_context.games.First(x => x.id == gameId));
            _context.SaveChanges();
            return true;
        }
    }
}
