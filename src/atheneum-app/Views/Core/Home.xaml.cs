using System;
using System.Linq;
using System.Threading.Tasks;
using atheneum_app.Library.DataAccess.Implementations;
using atheneum_app.Library.Extensions;
using atheneum_app.Library.Models.View;
using atheneum_app.Utils;
using atheneum_app.Views.Books;
using Refit;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace atheneum_app.Views.Core
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentView
    {
        private readonly BookService _bookService;
        private readonly AuthorService _authorService;
        private readonly PublisherService _publisherService;
        private readonly StatisticsService _statisticsService;

        public Home()
        {
            InitializeComponent();
            _bookService = BookService.Instance();
            _authorService = AuthorService.Instance();
            _publisherService = PublisherService.Instance();
            _statisticsService = StatisticsService.Instance();

            // set the header
            var (firstName, _) = TokenService.GetUserDetails();
            lblName.Text = firstName?.Trim() + ".";
        }

        public async Task LoadData()
        {
            // load remote data
            const string genericErrorMessage =
                "Sorry, an error occurred when retrieving your data. Try again later.";
            rfsHome.IsRefreshing = true;

            try
            {
                // get the most recent books
                var bookResponse = await _bookService.GetRecent();
                var books = bookResponse.ToList();

                if (books.Any())
                {
                    lstRecentBooks.ItemsSource = books;
                    lblNoRecentBooks.IsVisible = false;
                }
                else
                {
                    lblNoRecentBooks.IsVisible = true;
                }

                // get the top authors
                var authorResponse = await _authorService.GetTop();
                var authors = authorResponse.ToList();

                if (authors.Any())
                {
                    lstTopAuthors.ItemsSource = authors;
                    lblNoAuthors.IsVisible = false;
                }
                else
                {
                    lblNoAuthors.IsVisible = true;
                }

                // get the top publishers
                var publisherResponse = await _publisherService.GetTop();
                var publishers = publisherResponse.ToList();

                if (publishers.Any())
                {
                    lstTopPublishers.ItemsSource = publishers;
                    lblNoPublishers.IsVisible = false;
                }
                else
                {
                    lblNoPublishers.IsVisible = true;
                }

                // get the stats for the books
                var stats = await _statisticsService.Get();
                lblBooksCount.Text = stats.Books.ToString();
                lblAuthorsCount.Text = stats.Authors.ToString();
                lblPublishersCount.Text = stats.Publishers.ToString();
                lblWishlistCount.Text = stats.WishListItems.ToString();
            }
            catch (ApiException ex) when (ex.IsValidationException())
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
                rfsHome.IsRefreshing = false;
            }
        }

        private async void Refresh(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async void BookSelected(object sender, SelectionChangedEventArgs e)
        {
            var book = lstRecentBooks.SelectedItem as BookViewModel;
            await Navigation.PushAsync(new Details(book));
        }

        private async void BuyMeACoffee(object sender, EventArgs e)
        {
            try
            {
                await Browser.OpenAsync("https://www.buymeacoffee.com/bolorundurowb",
                    BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private async void ViewSource(object sender, EventArgs e)
        {
            try
            {
                await Browser.OpenAsync("https://github.com/bolorundurowb/atheneum-app",
                    BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private async void GiveFeedback(object sender, EventArgs e)
        {
            try
            {
                await Browser.OpenAsync("https://github.com/bolorundurowb/atheneum-app/issues/new",
                    BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
