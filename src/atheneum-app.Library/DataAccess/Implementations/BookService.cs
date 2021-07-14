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
        private int _currentPage;
        public const int BooksPerPage = 30;
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
            _currentPage = 1;
            return MakeApiCall();
        }

        public Task<IEnumerable<BookViewModel>> GetNextPage()
        {
            _currentPage += 1;
            return MakeApiCall();
        }

        private Task<IEnumerable<BookViewModel>> MakeApiCall()
        {
            var skip = (_currentPage - 1) * BooksPerPage;
            return _bookService.GetAll(skip, BooksPerPage);
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