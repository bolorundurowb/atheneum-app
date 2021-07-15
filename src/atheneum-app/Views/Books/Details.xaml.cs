using System;
using atheneum_app.Library.DataAccess.Implementations;
using atheneum_app.Library.Models.View;
using atheneum_app.Utils;
using Refit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace atheneum_app.Views.Books
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Details : ContentPage
    {
        private readonly BookService _bookService;
        private readonly BookViewModel _book;

        public Details(BookViewModel book)
        {
            InitializeComponent();
            _book = book;
            _bookService = new BookService();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = _book;
        }

        protected async void Remove(object sender, EventArgs e)
        {
            const string genericErrorMessage =
                "Sorry, an error occurred when removing from your library. Try again later.";

            try
            {
                await _bookService.Remove(_book.Id);
                ToastService.Success("Book successfully removed from your library.");
                await Navigation.PopAsync();
            }
            catch (ApiException ex)
            {
                var error = await ex.GetContentAsAsync<ErrorViewModel>();
                ToastService.Error(error?.Message?.Length > 0 ? error.Message : genericErrorMessage);
            }
        }

        protected async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}