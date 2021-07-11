using System.Collections.Generic;
using System.Threading.Tasks;
using atheneum_app.Library.DataAccess.Interfaces;
using atheneum_app.Library.Models.Binding;
using atheneum_app.Library.Models.View;
using Refit;

namespace atheneum_app.Library.DataAccess.Implementations
{
    public sealed class WishListService
    {
        private readonly IWishListService _wishListService;

        public WishListService()
        {
            var tokenClient = new TokenService();
            _wishListService = RestService.For<IWishListService>(Constants.V1BaseUrl, new RefitSettings
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(tokenClient.GetToken())
            });
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