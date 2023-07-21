using System.Collections.Generic;
using System.Threading.Tasks;
using AtheneumApp.Library.DataAccess.Interfaces;
using AtheneumApp.Library.Models.Binding;
using AtheneumApp.Library.Models.View;
using Refit;

namespace AtheneumApp.Library.DataAccess.Implementations
{
    public class BookService
    {
        private int _currentPage;
        public const int BooksPerPage = 30;
        private readonly IBookService _bookService;
        private static BookService _instance;

        private BookService()
        {
            _bookService = RestService.For<IBookService>(Constants.V1BaseUrl, new RefitSettings
            {
                AuthorizationHeaderValueGetter = TokenService.GetAuthToken
            });
        }

        public static BookService Instance()
        {
            return _instance ??= new BookService();
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

        public Task AddByIsbn(string isbn)
        {
            return _bookService.AddByIsbn(new CreateIsbnBookBindingModel
            {
                Isbn = isbn
            });
        }

        public Task AddManual(string title, string summary, string isbn, string publishYear, string authors,
            string publisher, string pageCount)
        {
            int? publishYearValue = null;
            int? pageCountValue = null;

            if (int.TryParse(publishYear, out var publishResult))
            {
                publishYearValue = publishResult;
            }

            if (int.TryParse(pageCount, out var pagResult))
            {
                pageCountValue = pagResult;
            }

            return _bookService.AddManual(new CreateManualBookBindingModel
            {
                Authors = authors,
                Isbn = isbn,
                Publisher = publisher,
                PageCount = pageCountValue,
                PublishYear = publishYearValue,
                Summary = summary,
                Title = title
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
