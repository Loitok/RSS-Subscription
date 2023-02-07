using Rss_Subscription.DataAccess.Implementations;
using System.Threading.Tasks;

namespace Rss_Subscription.DataAccess.Abstractions
{
    public interface IUnitOfWork
    {
        Repository<T> Repository<T>() where T : class;
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
