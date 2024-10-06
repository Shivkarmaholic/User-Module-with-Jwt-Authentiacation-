using System.ComponentModel.DataAnnotations;

namespace KarmaBook.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; } 
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Roles { get; set; } = Role.User | Role.Seller;
    }

    public class UserProfile: User
    {
        public string? ProfileSummary { get; set; }
    }
}
