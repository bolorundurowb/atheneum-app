using System.Collections.Generic;
using System.Threading.Tasks;
using AtheneumApp.Library.DataAccess.Interfaces;
using AtheneumApp.Library.Models.Binding;
using AtheneumApp.Library.Models.View;
using Refit;

namespace AtheneumApp.Library.DataAccess.Implementations
{
    public sealed class WishListService
    {
        private readonly IWishListService _wishListService;
        private static WishListService _instance;

        private WishListService()
        {
            _wishListService = RestService.For<IWishListService>(Constants.V1BaseUrl, new RefitSettings
            {
                AuthorizationHeaderValueGetter = TokenService.GetAuthToken
            });
        }

        public static WishListService Instance()
        {
            return _instance ??= new WishListService();                          
        }

        public Task<IEnumerable<WishListViewModel>> GetAll()
        {
            return _wishListService.GetAll();
        }

        public Task<WishListViewModel> Add(string title, string author, string isbn)
        {
            return _wishListService.Add(new AddWishListBindingModel
            {
                BookTitle = title,
                BookAuthor = author,
                BookIsbn = isbn
            });
        }
    }
}
