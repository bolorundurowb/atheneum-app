using System.Threading.Tasks;
using atheneum_app.Android.Services.Implementations;
using atheneum_app.Services.Interfaces;
using Java.Lang;
using Xamarin.Forms;
using ZXing.Mobile;
using Application = Android.App.Application;

[assembly: Dependency(typeof(BarcodeScanService))]
namespace atheneum_app.Android.Services.Implementations
{
    public class BarcodeScanService : IBarcodeScanService
    {
        public async Task<string> ScanAsync()
        {
            try
            {
                var options = new MobileBarcodeScanningOptions
                {
                    AutoRotate = false,
                    UseNativeScanning = true
                };
                var scanner = new MobileBarcodeScanner
                {
                    TopText = "Scan the ISBN bar code on your book",
                    BottomText = "Scanning..."
                };

                var scanResult = await scanner.Scan(Application.Context, options);
                return scanResult?.Text;
            }
            catch (Exception)
            {
                return null;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}