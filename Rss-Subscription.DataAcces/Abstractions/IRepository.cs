using System.Linq;
using System.Threading.Tasks;

namespace Rss_Subscription.DataAccess.Abstractions
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All { get; }

        Task<int> SaveChangesAsync();

        Task InsertAsync(T entity);

    }
}
