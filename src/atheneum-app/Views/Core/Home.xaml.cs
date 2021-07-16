using System.Linq;
using System.Net;
using System.Threading.Tasks;
using atheneum_app.Library.DataAccess.Implementations;
using atheneum_app.Library.Models.View;
using atheneum_app.Utils;
using Refit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace atheneum_app.Views.Core
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentView
    {
        private readonly TokenService _tokenService;
        private readonly BookService _bookService;

        public Home()
        {
            InitializeComponent();
            _tokenService = new TokenService();
            _bookService = BookService.Instance();
        }

        public async Task LoadData()
        {
            // set the header
            var (firstName, _) = _tokenService.GetUserDetails();
            lblName.Text = firstName;

            // load remote data
            const string genericErrorMessage =
                "Sorry, an error occurred when retrieving your data. Try again later.";
            prgLoading.IsVisible = true;

            try
            {
                // get the most recent books
                var response = await _bookService.GetRecent();
                var books = response.ToList();

                if (books.Any())
                {
                    lstRecentBooks.ItemsSource = books;
                    lblNoRecentBooks.IsVisible = false;
                }
                else
                {
                    lblNoRecentBooks.IsVisible = true;
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