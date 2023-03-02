using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class reg
    {
        [Key]
        public int Users_id { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
    }
}
