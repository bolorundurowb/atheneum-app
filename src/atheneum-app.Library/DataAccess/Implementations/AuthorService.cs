using System.Collections.Generic;
using System.Threading.Tasks;
using atheneum_app.Library.DataAccess.Interfaces;
using atheneum_app.Library.Models.Binding;
using atheneum_app.Library.Models.View;
using Refit;

namespace atheneum_app.Library.DataAccess.Implementations
{
    public class AuthorService
    {
        private readonly IAuthorService _authorService;

        public AuthorService()
        {
            var tokenClient = new TokenService();
            _authorService = RestService.For<IAuthorService>(Constants.V1BaseUrl, new RefitSettings
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(tokenClient.GetToken())
            });
        }

        public Task<IEnumerable<TopAuthorViewModel>> GetTop()
        {
            return _authorService.GetTop();
        }
    }
}