using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeedrunsAngular.Models
{
    public class SpeedrunModel
    {
        public int id { get; set; }
        public int position { get; set; }
        public string username { get; set; }
        public string country { get; set; }
        public string time { get; set; }
        public string date { get; set; }
        public string platform { get; set; }
        public string category { get; set; }

    }
}
