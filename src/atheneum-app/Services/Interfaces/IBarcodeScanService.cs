using System.Threading.Tasks;

namespace atheneum_app.Services.Interfaces
{
    public interface IBarcodeScanService
    {
        Task<string> ScanAsync();
    }
}