using System.Collections.Generic;
using System.Threading.Tasks;
using atheneum_app.Library.Models.View;
using Refit;

namespace atheneum_app.Library.DataAccess.Interfaces
{
    public interface IAuthorService
    {
        [Get("/authors")]
        [Headers("Authorization: Bearer")]
        Task<IEnumerable<AuthorViewModel>> GetAll();
    }
}