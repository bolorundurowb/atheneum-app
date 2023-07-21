using System;
using AtheneumApp.Library.DataAccess.Implementations;
using AtheneumApp.Library.Extensions;
using AtheneumApp.Library.Models.View;
using AtheneumApp.Utils;
using Refit;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms.Xaml;

namespace AtheneumApp.Views.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddWishList : Popup
    {
        private readonly WishListService _wishListService;

        public AddWishList()
        {
            InitializeComponent();
            _wishListService = WishListService.Instance();
        }

        protected async void Add(object sender, EventArgs e)
        {
            const string genericErrorMessage = "Sorry, an error occurred when adding the item. Try again later.";
            var title = txtTitle.Text;
            var author = txtAuthor.Text;
            var isbn = txtIsbn.Text;

            if (string.IsNullOrWhiteSpace(title))
            {
                ToastService.Error("A book title is required.");
                return;
            }

            if (string.IsNullOrWhiteSpace(author))
            {
                ToastService.Error("An author name is required.");
                return;
            }

            stkBtns.IsVisible = false;
            prgAdding.IsVisible = true;

            try
            {
                var response = await _wishListService.Add(title, author, isbn);

                // notify user
                ToastService.Success("Book successfully added to your wish list.");

                // send data back
                Dismiss(response);
            }
            catch (ApiException ex) when (ex.IsValidationException())
            {
                var error = await ex.GetContentAsAsync<ValidationErrorViewModel>();
                ToastService.Error(error?.Message?.Length > 0 ? error.Message[0] : genericErrorMessage);
            }
            catch (ApiException ex)
            {
                var error = await ex.GetContentAsAsync<ErrorViewModel>();
                ToastService.Error(!string.IsNullOrWhiteSpace(error?.Message) ? error.Message : genericErrorMessage);
            }
            finally
            {
                stkBtns.IsVisible = true;
                prgAdding.IsVisible = false;
            }
        }

        protected void Cancel(object sender, EventArgs e)
        {
            Dismiss(null);
        }

        protected override object GetLightDismissResult()
        {
            return null;
        }
    }
}
