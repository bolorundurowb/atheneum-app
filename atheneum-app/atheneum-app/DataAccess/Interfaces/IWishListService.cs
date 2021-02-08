using System.Collections.Generic;
using System.Threading.Tasks;
using atheneum_app.Models.Binding;
using atheneum_app.Models.View;
using Refit;

namespace atheneum_app.DataAccess.Interfaces
{
    public interface IWishListService
    {
        [Get("/wish-list")]
        [Headers("Authorization: Bearer")]
        Task<IEnumerable<WishListViewModel>> GetAll();
        
        [Post("/wish-list")]
        [Headers("Authorization: Bearer")]
        Task<WishListViewModel> Add([Body] AddWishListBindingModel payload);
        
        [Delete("/wish-list/{wishId}")]
        [Headers("Authorization: Bearer")]
        Task<WishListViewModel> Delete(string wishId);
    }
}