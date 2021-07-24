using System.Threading.Tasks;
using atheneum_app.Library.Models.View;
using Refit;

namespace atheneum_app.Library.DataAccess.Interfaces
{
    public interface IStatisticsService
    {
        [Get("/statistics")]
        [Headers("Authorization: Bearer")]
        Task<StatisticsViewModel> GetAll();
    }
}
