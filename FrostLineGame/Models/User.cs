using System.ComponentModel.DataAnnotations;

namespace FrostLineGame.Models
{
    public class User
    {

        [Key]
        public int UserID { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string MailAdress { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; } 


    }
}
