using BankingPortal.Domain.Entities.Base;
namespace BankingPortal.Domain.Entities
{
    public class Role:AuditableEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
