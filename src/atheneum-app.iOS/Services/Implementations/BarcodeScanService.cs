using System;
using System.Threading.Tasks;
using atheneum_app.iOS.Services.Implementations;
using atheneum_app.Services.Interfaces;
using Foundation;
using Xamarin.Forms;
using ZXing.Mobile;

[assembly: Dependency(typeof(BarcodeScanService))]
namespace atheneum_app.iOS.Services.Implementations
{
    public class BarcodeScanService :  IBarcodeScanService
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
                    TopText = "Scan the QR Code",
                    BottomText = "Scanning..."
                };

                var scanResult = await scanner.Scan(options, true);
                return scanResult?.Text;
            }
            catch (NSErrorException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}