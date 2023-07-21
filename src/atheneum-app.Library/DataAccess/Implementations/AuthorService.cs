using System.Collections.Generic;
using System.Threading.Tasks;
using AtheneumApp.Library.DataAccess.Interfaces;
using AtheneumApp.Library.Models.View;
using Refit;

namespace AtheneumApp.Library.DataAccess.Implementations
{
    public class AuthorService
    {
        private readonly IAuthorService _authorService;
        private static AuthorService _instance;

        private AuthorService() =>
            _authorService = RestService.For<IAuthorService>(Constants.V1BaseUrl, new RefitSettings
            {
                AuthorizationHeaderValueGetter = TokenService.GetAuthToken
            });

        public static AuthorService Instance() => _instance ?? (_instance = new AuthorService());

        public Task<IEnumerable<TopAuthorViewModel>> GetTop() => _authorService.GetTop();
    }
}
