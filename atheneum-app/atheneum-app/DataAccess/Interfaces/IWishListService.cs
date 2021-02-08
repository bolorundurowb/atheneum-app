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
        Task<IEnumerable<WishListViewModel>> GetAll();
        
        [Post("/wish-list")]
        Task<WishListViewModel> Add([Body] AddWishListBindingModel payload);
    }
}