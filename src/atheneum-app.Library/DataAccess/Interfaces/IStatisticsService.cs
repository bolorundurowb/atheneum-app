using System.Threading.Tasks;
using AtheneumApp.Library.Models.View;
using Refit;

namespace AtheneumApp.Library.DataAccess.Interfaces
{
    public interface IStatisticsService
    {
        [Get("/statistics")]
        [Headers("Authorization: Bearer")]
        Task<StatisticsViewModel> Get();
    }
}
