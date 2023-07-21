using System.Collections.Generic;
using System.Threading.Tasks;
using AtheneumApp.Library.Models.Binding;
using AtheneumApp.Library.Models.View;
using Refit;

namespace AtheneumApp.Library.DataAccess.Interfaces
{
    public interface IBookService
    {
        [Get("/books")]
        [Headers("Authorization: Bearer")]
        Task<IEnumerable<BookViewModel>> GetAll([AliasAs("skip")] int skip, [AliasAs("limit")] int limit,
            [AliasAs("search")] string search = "");

        [Get("/books/recent")]
        [Headers("Authorization: Bearer")]
        Task<IEnumerable<BookViewModel>> GetRecent();

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
