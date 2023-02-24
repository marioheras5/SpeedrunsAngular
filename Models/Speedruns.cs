using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeedrunsAngular.Models
{
    public class Speedruns
    {
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
