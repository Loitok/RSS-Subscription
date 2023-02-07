using Rss_Subscription.BLL.DTOs.Result.Abstractions;
using System;

namespace Rss_Subscription.BLL.DTOs.Result.Implementations
{
    internal class Result : IResult
    {
        public bool Success { get; private set; }
        public ResponseMessage ErrorMessage { get; private set; }

        private Result() { }

        public static Result CreateSuccess()
            => new Result
            {
                Success = true
            };

        public static Result CreateFailure(string message)
        {
            return new Result
            {
                Success = false,
                ErrorMessage = new ResponseMessage(message)
            };
        }

        public static Result CreateFailure(ResponseMessage message)
        {
            return new Result
            {
                Success = false,
                ErrorMessage = message
            };
        }

        public static Result CreateFailure(string message, Exception exception)
        {
            return new Result
            {
                Success = false,
                ErrorMessage = new ResponseMessage(message)
            };
        }
    }
}
