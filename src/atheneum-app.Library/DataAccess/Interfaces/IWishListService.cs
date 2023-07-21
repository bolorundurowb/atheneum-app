using System.Collections.Generic;
using System.Threading.Tasks;
using AtheneumApp.Library.Models.Binding;
using AtheneumApp.Library.Models.View;
using Refit;

namespace AtheneumApp.Library.DataAccess.Interfaces
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