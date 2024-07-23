using System.ComponentModel.DataAnnotations;

namespace RM.User.Model.Entities
{
    public class RegistrationRequest
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
        public string  FirstName { get; set; }
        public string LastName { get; set; }

        public Role Role { get; set; }
    }
}
