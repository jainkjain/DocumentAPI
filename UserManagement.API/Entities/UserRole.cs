using System.ComponentModel.DataAnnotations;

namespace UserManagement.API.Entities
{
    public class UserRole
    {
        [Key]
        public int RoleId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
