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
        Task<IEnumerable<BookViewModel>> GetAll();

        [Get("/books/isbn")]
        [Headers("Authorization: Bearer")]
        Task<BookViewModel> AddByIsbn([Body] CreateIsbnBookBindingModel payload);

        [Get("/books/manual")]
        [Headers("Authorization: Bearer")]
        Task<BookViewModel> AddManual([Body] CreateManualBookBindingModel payload);

        [Get("/books/{bookId}/borrow")]
        [Headers("Authorization: Bearer")]
        Task<BookViewModel> Borrow(string bookId, [Body] BorrowBookBindingModel payload);

        [Get("/books/{bookId}/return")]
        [Headers("Authorization: Bearer")]
        Task<BookViewModel> Return(string bookId);
    }
}