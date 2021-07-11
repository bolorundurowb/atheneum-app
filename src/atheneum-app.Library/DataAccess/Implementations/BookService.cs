using System.Collections.Generic;
using System.Threading.Tasks;
using atheneum_app.Library.DataAccess.Interfaces;
using atheneum_app.Library.Models.View;
using Refit;

namespace atheneum_app.Library.DataAccess.Implementations
{
    public class BookService
    {
        private readonly IBookService _bookService;

        public BookService()
        {
            var tokenClient = new TokenService();
            _bookService = RestService.For<IBookService>(Constants.V1BaseUrl, new RefitSettings
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(tokenClient.GetToken())
            });
        }

        public Task<IEnumerable<BookViewModel>> GetAll()
        {
            return _bookService.GetAll(0, 30);
        }
    }
}