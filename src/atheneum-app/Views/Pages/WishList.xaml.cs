﻿using System;
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

namespace atheneum_app.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WishList : ContentView
    {
        public ObservableCollection<WishListViewModel> WishListItems =
            new ObservableCollection<WishListViewModel>(Enumerable.Empty<WishListViewModel>());

        private readonly WishListService _wishListService;

        public WishList()
        {
            InitializeComponent();
            _wishListService = new WishListService();
        }

        public async Task LoadData()
        {
            const string genericErrorMessage =
                "Sorry, an error occurred when retrieving your wish list. Try again later.";
            rfsWishList.IsRefreshing = true;

            try
            {
                var wishlist = await _wishListService.GetAll();

                if (wishlist.Any())
                {
                    WishListItems = new ObservableCollection<WishListViewModel>(wishlist);
                    lstWishList.ItemsSource = WishListItems;
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
                ToastService.Info("Modal dismissed.");
                return;
            }

            // hide the no items label if it is visible
            lblNoItems.IsVisible = false;
            lstWishList.IsVisible = true;

            // add the result in
            WishListItems.Insert(0, result);
            lstWishList.ItemsSource = WishListItems;
        }

        private async void Refreshing(object sender, EventArgs e)
        {
            await LoadData();
        }
    }
}