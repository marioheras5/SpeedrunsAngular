using System.ComponentModel.DataAnnotations;

namespace SpeedrunsAngular.Models
{
    public class Users
    {
        public Users(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        [Key]
        public string username { get; set; }
        public string password { get; set; }
    }
}
