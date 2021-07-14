using atheneum_app.Library.Models.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace atheneum_app.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookDetails : ContentPage
    {
        private readonly BookViewModel _book;

        public BookDetails(BookViewModel book)
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