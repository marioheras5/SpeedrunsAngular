using System.ComponentModel.DataAnnotations;

namespace SpeedrunsAngular.Models
{
    public class Games
    {
        public Games(int id, string name, string shortName, string img)
        {
            this.id = id;
            this.name = name;
            this.shortName = shortName;
            this.img = img;
        }
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string img { get; set; }
    }
}
