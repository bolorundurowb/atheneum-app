using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using atheneum_app.Library.DataAccess.Implementations;
using atheneum_app.Library.Models.View;
using atheneum_app.Utils;
using Refit;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace atheneum_app.Views.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddWishList : Popup
    {
        private readonly WishListService _wishListService;
        
        public AddWishList()
        {
            InitializeComponent();
            _wishListService = new WishListService();
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
            catch (ApiException ex) when (ex.StatusCode is HttpStatusCode.BadRequest)
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