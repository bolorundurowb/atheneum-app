using System.Collections.Generic;
using System.Threading.Tasks;
using atheneum_app.Library.Models.Binding;
using atheneum_app.Library.Models.View;
using Refit;

namespace atheneum_app.Library.DataAccess.Interfaces
{
    public interface IBookService
    {
        [Get("/books")]
        [Headers("Authorization: Bearer")]
        Task<IEnumerable<BookViewModel>> GetAll([AliasAs("skip")] int skip, [AliasAs("limit")] int limit, [AliasAs("search")] string search = "");

        [Post("/books/isbn")]
        [Headers("Authorization: Bearer")]
        Task AddByIsbn([Body] CreateIsbnBookBindingModel payload);

        [Post("/books/manual")]
        [Headers("Authorization: Bearer")]
        Task<BookViewModel> AddManual([Body] CreateManualBookBindingModel payload);

        [Post("/books/{bookId}/borrow")]
        [Headers("Authorization: Bearer")]
        Task<BookViewModel> Borrow(string bookId, [Body] BorrowBookBindingModel payload);

        [Post("/books/{bookId}/return")]
        [Headers("Authorization: Bearer")]
        Task<BookViewModel> Return(string bookId);

        [Delete("/books/{bookId}")]
        [Headers("Authorization: Bearer")]
        Task Remove(string bookId);
    }
}