using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeedrunsAngular.Models
{
    public class Speedruns
    {
        public Speedruns(int id, int id_game, string username, string country, TimeSpan time, DateTime date, string platform, string category)
        {
            this.id = id;
            this.id_game = id_game;
            this.username = username;
            this.country = country;
            this.time = time;
            this.date = date;
            this.platform = platform;
            this.category = category;
        }
        [Key]
        public int id { get; set; }
        [ForeignKey("game")]
        public int id_game { get; set; }
        public Games game { get; set; }
        
        [ForeignKey("user")]
        public string username { get; set; }
        public Users user { get; set; }
        public string country { get; set; }
        public TimeSpan time { get; set; }
        public DateTime date { get; set; }
        public string platform { get; set; }
        public string category { get; set; }
        
    }
}
