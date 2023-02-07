namespace Rss_Subscription.BLL.DTOs.Result.Abstractions.Generics
{
    public interface IResult<out TData> : IResult
    {
        TData Data { get; }
    }
}
