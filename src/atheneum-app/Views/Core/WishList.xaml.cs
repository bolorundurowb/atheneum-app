using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using atheneum_app.Library.DataAccess.Implementations;
using atheneum_app.Library.Models.View;
using atheneum_app.Utils;
using atheneum_app.Views.Modals;
using Refit;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace atheneum_app.Views.Core
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WishList : ContentView
    {
        private ObservableCollection<WishListViewModel> _wishListItems = new ObservableCollection<WishListViewModel>();
        private readonly WishListService _wishListService;

        public WishList()
        {
            InitializeComponent();
            _wishListService = WishListService.Instance();
        }

        public async Task LoadData()
        {
            const string genericErrorMessage =
                "Sorry, an error occurred when retrieving your wish list. Try again later.";
            rfsWishList.IsRefreshing = true;

            try
            {
                var response = await _wishListService.GetAll();
                var wishlist = response.ToList();

                if (wishlist.Any())
                {
                    _wishListItems = new ObservableCollection<WishListViewModel>(wishlist);
                    lstWishList.ItemsSource = _wishListItems;
                }
                else
                {
                    lblNoItems.IsVisible = true;
                }
            }
            catch (ApiException ex) when (ex.StatusCode is HttpStatusCode.BadRequest)
            {
                var error = await ex.GetContentAsAsync<ValidationErrorViewModel>();
                ToastService.Error(error?.Message?.Length > 0 ? error.Message[0] : genericErrorMessage);
            }
            catch (ApiException ex)
            {
                var error = await ex.GetContentAsAsync<ErrorViewModel>();
                ToastService.Error(error?.Message?.Length > 0 ? error.Message : genericErrorMessage);
            }
            finally
            {
                rfsWishList.IsRefreshing = false;
            }
        }

        protected async void Add(object sender, EventArgs e)
        {
            var result = await Navigation.ShowPopupAsync(new AddWishList()) as WishListViewModel;

            if (result == null)
            {
                return;
            }

            // hide the no items label if it is visible
            lblNoItems.IsVisible = false;
            lstWishList.IsVisible = true;

            // add the result in
            _wishListItems.Insert(0, result);
            lstWishList.ItemsSource = _wishListItems;
        }

        protected async void Refreshing(object sender, EventArgs e)
        {
            await LoadData();
        }
    }
}