

namespace BankingPortal.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
        public DateTime? Expiration { get; set; }

        public static RefreshToken Create(string token, int userId, DateTime? expiration)
        {
            return new RefreshToken
            {

                Token = token,
                UserId = userId,
                Expiration = expiration
            };
        }
    }
}
