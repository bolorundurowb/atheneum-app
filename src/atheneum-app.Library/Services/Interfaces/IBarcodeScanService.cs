using System.Threading.Tasks;

namespace atheneum_app.Library.Services.Interfaces
{
    public interface IBarcodeScanService
    {
        Task<string> ScanAsync();
    }
}