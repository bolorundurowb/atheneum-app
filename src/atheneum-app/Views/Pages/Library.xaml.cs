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
        private bool IsLoadingMore;

        public Library()
        {
            InitializeComponent();
            _bookService = new BookService();
        }

        public async Task LoadData()
        {
            const string genericErrorMessage =
                "Sorry, an error occurred when retrieving your library. Try again later.";
            rfsLibrary.IsRefreshing = true;

            try
            {
                var response = await _bookService.GetFirstPage();
                var books = response.ToList();

                if (books.Any())
                {
                    Books = new ObservableCollection<BookViewModel>(books);
                    lstBooks.ItemsSource = Books;
                    lblNoItems.IsVisible = false;
                }
                else
                {
                    Books.Clear();
                    lstBooks.ItemsSource = Books;
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

        private async void LoadMore(object sender, EventArgs e)
        {
            const string genericErrorMessage =
                "Sorry, an error occurred when retrieving more library items. Try again later.";

            try
            {
                // ensure multiple calls are not made until the current is done
                if (IsLoadingMore)
                {
                    return;
                }
                
                // check to see if the list is less than than the max items per page meaning there
                // are no more items to get
                if (Books.Count % BookService.BooksPerPage != 0)
                {
                    return;
                }

                IsLoadingMore = true;
                prgLoadingMore.IsVisible = true;
                var response = await _bookService.GetNextPage();
                var books = response.ToList();

                if (books.Any())
                {
                    foreach (var book in books)
                    {
                        Books.Add(book);
                    }

                    lstBooks.ItemsSource = Books;
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
                IsLoadingMore = false;
                prgLoadingMore.IsVisible = false;
            }
        }
    }
}