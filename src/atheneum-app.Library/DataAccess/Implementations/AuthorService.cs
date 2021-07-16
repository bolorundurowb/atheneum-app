using System.Collections.Generic;
using System.Threading.Tasks;
using atheneum_app.Library.DataAccess.Interfaces;
using atheneum_app.Library.Models.View;
using Refit;

namespace atheneum_app.Library.DataAccess.Implementations
{
    public class AuthorService
    {
        private readonly IAuthorService _authorService;
        private static AuthorService _instance;

        private AuthorService()
        {
            var tokenClient = new TokenService();
            _authorService = RestService.For<IAuthorService>(Constants.V1BaseUrl, new RefitSettings
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(tokenClient.GetToken())
            });
        }

        public static AuthorService Instance()
        {
            return _instance ?? (_instance = new AuthorService());
        }

        public Task<IEnumerable<TopAuthorViewModel>> GetTop()
        {
            return _authorService.GetTop();
        }
    }
}