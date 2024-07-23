using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RM.User.Model.Entities
{
    public class Utilisateur : IdentityUser
    {
        [Key]
        public string PartitionKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }

        public Utilisateur()
        {
            PartitionKey = Id.ToString();
        }
    }

}
