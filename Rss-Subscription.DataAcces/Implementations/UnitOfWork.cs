using Rss_Subscription.DataAccess.Abstractions;
using Rss_Subscription.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rss_Subscription.DataAccess.Implementations
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private Dictionary<string, object> _repositories;
        private bool _disposed;


        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveChanges()
            => _dbContext.SaveChanges();

        public Task SaveChangesAsync()
            => _dbContext.SaveChangesAsync();

        public void Dispose()
        {
            if (!_disposed)
            {
                _dbContext.Dispose();
                _disposed = true;
            }

            GC.SuppressFinalize(this);
        }

        public Repository<T> Repository<T>() where T : class
        {
            _repositories ??= new Dictionary<string, object>();

            var type = typeof(T).Name;

            if (_repositories.ContainsKey(type))
                return _repositories[type] as Repository<T>;

            var repositoryType = typeof(Repository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbContext);

            _repositories.Add(type, repositoryInstance);

            return _repositories[type] as Repository<T>;
        }
    }
}
