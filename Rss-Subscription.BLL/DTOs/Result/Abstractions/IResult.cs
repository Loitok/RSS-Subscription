using Rss_Subscription.BLL.DTOs.Result.Implementations;

namespace Rss_Subscription.BLL.DTOs.Result.Abstractions
{
    public interface IResult
    {
        bool Success { get; }
        ResponseMessage ErrorMessage { get; }
    }
}
