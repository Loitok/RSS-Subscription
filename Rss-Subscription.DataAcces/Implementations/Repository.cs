using Microsoft.EntityFrameworkCore;
using Rss_Subscription.DataAccess.Abstractions;
using Rss_Subscription.DataAccess.Contexts;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rss_Subscription.DataAccess.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _entities;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = dbContext.Set<T>();
        }

        public IQueryable<T> All => _entities;

        public Task<int> SaveChangesAsync()
            => _dbContext.SaveChangesAsync();


        public async Task InsertAsync(T entity)
        {
            try
            {
                InsertOnSave(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                HandleDbUpdateException(exception);
            }
        }

        public void InsertOnSave(T entity)
        {
            if (entity == null)
                return;

            _entities.Add(entity);
        }


        private static void HandleDbUpdateException(DbUpdateException dbu)
        {
            var builder = new StringBuilder("A DbUpdateException was caught while saving changes. ");

            try
            {
                foreach (var result in dbu.Entries)
                {
                    builder.AppendFormat("Type: {0} was part of the problem. ", result.Entity.GetType().Name);
                }
            }
            catch (Exception e)
            {
                builder.Append("Error parsing DbUpdateException: " + e.ToString());
            }

            string message = builder.ToString();
            throw new Exception(message, dbu);
        }
    }
}
