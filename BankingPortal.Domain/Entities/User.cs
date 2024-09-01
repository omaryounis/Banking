
using BankingPortal.Domain.Enum;
using BankingPortal.Domain.Entities.Base;
namespace BankingPortal.Domain.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public byte [] Password { get; set; }
        public byte [] PasswordSalt { get; set; }
        public Guid TrackingCorrelationId { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        private readonly List<UserRole> _userRoles = new List<UserRole>();
        // Factory method to create a user and encapsulate business logic
        public static User Create(string userName, byte[] password, byte[] passwordSalt,Guid trackingCorrelationId)
        {
            // Password hashing and validation can happen here
            return new User
            {
                UserName = userName,
                Password = password,
                PasswordSalt = passwordSalt,
                TrackingCorrelationId = trackingCorrelationId
            };
        }
        // Add a role to the user
        public void AssignRole(int roleId)
        {
            if (!_userRoles.Any(ur => ur.RoleId == roleId))
            {
                _userRoles.Add(new UserRole { RoleId = roleId, UserId = this.Id });
            }
        }
    }
}
