
using BankingPortal.Domain.Enum;
using BankingPortal.Domain.Entities.Base;
namespace BankingPortal.Domain.Entities
{
    public class User : AuditableEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string LastName { get; set; }
        public string LastNameAr { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }
        public byte [] Password { get; set; }
        public byte [] PasswordSalt { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public string UserName { get; set; }
        public User()
        {
            CreatedBy = 1;
            CreatedDate=DateTime.Now;
        }

    }
}
