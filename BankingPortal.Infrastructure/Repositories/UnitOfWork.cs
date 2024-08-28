using BankingPortal.Domain.Interfaces;
using BankingPortal.EntityFrameWorkCore;

namespace BankingPortal.Infrastructure.Extensions.Repositories
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly BankDbContext _context;
        private readonly Dictionary<string, object> _repositories;

        public UnitOfWork(BankDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<string, object>();
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                _repositories[type] = repositoryInstance;
            }
            return (IGenericRepository<T>)_repositories[type];
        }
        
    }
}
