using System.Net;
using Refit;

namespace atheneum_app.Library.Extensions
{
    public static class ApiExceptionExtensions
    {
        private const string OpenParen = "[";
        private const string CloseParen = "[";

        public static bool IsValidationException(this ApiException ex)
        {
            return ex.StatusCode == HttpStatusCode.BadRequest
                   && ex.Content != null
                   && ex.Content.Contains(OpenParen)
                   && ex.Content.Contains(CloseParen);
        }
    }
}
