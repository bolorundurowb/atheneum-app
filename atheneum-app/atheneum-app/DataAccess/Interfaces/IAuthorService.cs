using System.Collections.Generic;
using System.Threading.Tasks;
using atheneum_app.Models.View;
using Refit;

namespace atheneum_app.DataAccess.Interfaces
{
    public interface IAuthorService
    {
        [Get("/authors")]
        [Headers("Authorization: Bearer")]
        Task<IEnumerable<AuthorViewModel>> GetAll();
    }
}