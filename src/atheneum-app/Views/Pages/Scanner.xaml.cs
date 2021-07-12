using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Mobile;

namespace atheneum_app.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Scanner : ContentPage
    {
        public string ScanResult { get; private set; }
        
        public Scanner()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            scanner.Options = new MobileBarcodeScanningOptions
            {
               AutoRotate = false
            };
        }

        private void OnScan(Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                ScanResult = result?.Text;
                await Navigation.PopAsync();
            });
        }
    }
}