using System.Linq;
using System.Net;
using System.Threading.Tasks;
using atheneum_app.Library.DataAccess.Implementations;
using atheneum_app.Library.Models.View;
using atheneum_app.Utils;
using Refit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace atheneum_app.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WishList : ContentView
    {
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
            prgLoading.IsVisible = true;
            
            try
            {
                var wishlist = await _wishListService.GetAll();

                if (wishlist.Any())
                {
                    
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
                prgLoading.IsVisible = false;
            }
        }
    }
}