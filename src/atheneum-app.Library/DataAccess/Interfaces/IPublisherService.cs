using System.Collections.Generic;
using System.Threading.Tasks;
using AtheneumApp.Library.Models.View;
using Refit;

namespace AtheneumApp.Library.DataAccess.Interfaces
{
    public interface IPublisherService
    {
        [Get("/publishers")]
        [Headers("Authorization: Bearer")]
        Task<IEnumerable<PublisherViewModel>> GetAll();
        
        [Get("/publishers/top")]
        [Headers("Authorization: Bearer")]
        Task<IEnumerable<TopPublisherViewModel>> GetTop();
    }
}