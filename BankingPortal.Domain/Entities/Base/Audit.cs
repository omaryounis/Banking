namespace BankingPortal.Domain.Entities.Base
{
    public abstract class Audit
    {
    
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }

}
