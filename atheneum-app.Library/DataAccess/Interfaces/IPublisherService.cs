using System.Collections.Generic;
using System.Threading.Tasks;
using atheneum_app.Library.Models.View;
using Refit;

namespace atheneum_app.Library.DataAccess.Interfaces
{
    public interface IPublisherService
    {
        [Get("/publishers")]
        [Headers("Authorization: Bearer")]
        Task<IEnumerable<PublisherViewModel>> GetAll();
    }
}