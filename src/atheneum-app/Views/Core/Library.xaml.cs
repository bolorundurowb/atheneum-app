using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using atheneum_app.Library.DataAccess.Implementations;
using atheneum_app.Library.Models.View;
using atheneum_app.Utils;
using atheneum_app.Views.Books;
using Refit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace atheneum_app.Views.Core
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Library : ContentView
    {
        private ObservableCollection<BookViewModel> _books = new ObservableCollection<BookViewModel>();
        private readonly BookService _bookService;
        private bool _isLoadingMore;

        public Library()
        {
            InitializeComponent();
            _bookService = BookService.Instance();
        }

        public async Task LoadData()
        {
            const string genericErrorMessage =
                "Sorry, an error occurred when retrieving your library. Try again later.";
            rfsLibrary.IsRefreshing = true;

            try
            {
                var search = GetSearchText();
                var response = await _bookService.GetFirstPage(search);
                var books = response.ToList();

                if (books.Any())
                {
                    _books = new ObservableCollection<BookViewModel>(books);
                    lstBooks.ItemsSource = _books;
                    lblNoItems.IsVisible = false;
                    lblNoSearchItems.IsVisible = false;
                }
                else
                {
                    _books.Clear();
                    lstBooks.ItemsSource = _books;

                    if (string.IsNullOrWhiteSpace(GetSearchText()))
                    {
                        lblNoItems.IsVisible = true;
                    }
                    else
                    {
                        lblNoSearchItems.IsVisible = true;
                    }
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
                if (_isLoadingMore)
                {
                    return;
                }

                // check to see if the list is less than than the max items per page meaning there
                // are no more items to get
                if (_books.Count % BookService.BooksPerPage != 0)
                {
                    return;
                }

                _isLoadingMore = true;
                prgLoadingMore.IsVisible = true;
                var search = GetSearchText();
                var response = await _bookService.GetNextPage(search);
                var books = response.ToList();

                if (books.Any())
                {
                    foreach (var book in books)
                    {
                        _books.Add(book);
                    }

                    lstBooks.ItemsSource = _books;
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
                _isLoadingMore = false;
                prgLoadingMore.IsVisible = false;
            }
        }

        private async void OnSearch(object sender, EventArgs e)
        {
            await LoadData();
        }

        private string GetSearchText()
        {
            return (txtSearch.Text ?? string.Empty).Trim();
        }

        private async void BookSelected(object sender, SelectionChangedEventArgs e)
        {
            var book = lstBooks.SelectedItem as BookViewModel;
            await Navigation.PushAsync(new Details(book));
        }
    }
}