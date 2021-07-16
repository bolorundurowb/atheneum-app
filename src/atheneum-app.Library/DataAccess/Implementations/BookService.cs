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

        public Task<IEnumerable<BookViewModel>> GetFirstPage(string search)
        {
            _currentPage = 1;
            return MakeApiCall(search);
        }

        public Task<IEnumerable<BookViewModel>> GetNextPage(string search)
        {
            _currentPage += 1;
            return MakeApiCall(search);
        }

        public Task<IEnumerable<BookViewModel>> GetRecent()
        {
            return _bookService.GetRecent();
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

        public Task Remove(string bookId)
        {
            return _bookService.Remove(bookId);
        }

        private Task<IEnumerable<BookViewModel>> MakeApiCall(string search)
        {
            var skip = (_currentPage - 1) * BooksPerPage;
            return _bookService.GetAll(skip, BooksPerPage, search);
        }
    }
}