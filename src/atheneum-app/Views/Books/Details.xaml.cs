using atheneum_app.Library.Models.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace atheneum_app.Views.Books
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Details : ContentPage
    {
        private readonly BookViewModel _book;

        public Details(BookViewModel book)
        {
            InitializeComponent();
            _book = book;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = _book;
        }
    }
}