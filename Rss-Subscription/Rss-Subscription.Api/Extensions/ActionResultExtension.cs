using Rss_Subscription.BLL.DTOs.Result.Abstractions;
using Rss_Subscription.BLL.DTOs.Result.Abstractions.Generics;
using System.Web.Http;
using System.Web.Http.Results;

namespace Rss_Subscription.Api.Extensions
{
    public static class ActionResultExtension
    {
        public static IHttpActionResult ToActionResult<T>(this IResult<T> result, ApiController apiController)
        {
            if (!result.Success)
            {
                //Logger<>.Error(result.ErrorMessage.Message);
                return new BadRequestErrorMessageResult(result.ErrorMessage.Message, apiController);
            }

            return new OkNegotiatedContentResult<T>(result.Data, apiController);
        }

        public static IHttpActionResult ToActionResult(this IResult result, ApiController apiController)
        {
            if (!result.Success)
            {
                //Logger.Error(result.ErrorMessage.Message);
                return new BadRequestErrorMessageResult(result.ErrorMessage.Message, apiController);
            }

            return new OkResult(apiController);
        }
    }
}
