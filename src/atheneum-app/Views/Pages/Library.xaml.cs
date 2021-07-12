using System;
using System.Collections.ObjectModel;
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
    public partial class Library : ContentView
    {
        public ObservableCollection<BookViewModel> Books = new ObservableCollection<BookViewModel>();
        private readonly BookService _bookService;

        public Library()
        {
            InitializeComponent();
            _bookService = new BookService();
        }

        public async Task LoadData()
        {
            const string genericErrorMessage =
                "Sorry, an error occurred when retrieving your library. Try again later.";
            lblNoItems.IsVisible = false;
            rfsLibrary.IsRefreshing = true;

            try
            {
                var books = await _bookService.GetAll();

                if (books.Any())
                {
                    Books = new ObservableCollection<BookViewModel>(books);
                    lstBooks.ItemsSource = Books;
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
                rfsLibrary.IsRefreshing = false;
            }
        }

        protected async void Refreshing(object sender, EventArgs e)
        {
            await LoadData();
        }
    }
}