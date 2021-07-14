using System.Collections.Generic;
using System.Threading.Tasks;
using atheneum_app.Library.DataAccess.Interfaces;
using atheneum_app.Library.Models.Binding;
using atheneum_app.Library.Models.View;
using Refit;

namespace atheneum_app.Library.DataAccess.Implementations
{
    public class BookService
    {
        private int currentPage;
        private const int booksPerPage = 30;
        private readonly IBookService _bookService;

        public BookService()
        {
            var tokenClient = new TokenService();
            _bookService = RestService.For<IBookService>(Constants.V1BaseUrl, new RefitSettings
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(tokenClient.GetToken())
            });
        }

        public Task<IEnumerable<BookViewModel>> GetFirstPage()
        {
            currentPage = 0;
            return MakeApiCall(currentPage, booksPerPage);
        }

        public Task<IEnumerable<BookViewModel>> GetNextPage()
        {
            currentPage += 1;
            return MakeApiCall(currentPage, booksPerPage);
        }

        private Task<IEnumerable<BookViewModel>> MakeApiCall(int skip, int limit)
        {
            return _bookService.GetAll(skip, limit);
        }

        public Task AddByIsbn(string isbn, double? longitude = null, double? latitude = null)
        {
            return _bookService.AddByIsbn(new CreateIsbnBookBindingModel
            {
                Isbn = isbn,
                Longitude = longitude,
                Latitude = latitude
            });
        }
    }
}