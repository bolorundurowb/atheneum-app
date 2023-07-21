using System.Collections.Generic;
using System.Threading.Tasks;
using AtheneumApp.Library.Models.View;
using Refit;

namespace AtheneumApp.Library.DataAccess.Interfaces
{
    public interface IAuthorService
    {
        [Get("/authors")]
        [Headers("Authorization: Bearer")]
        Task<IEnumerable<AuthorViewModel>> GetAll();

        [Get("/authors/top")]
        [Headers("Authorization: Bearer")]
        Task<IEnumerable<TopAuthorViewModel>> GetTop();
    }
}