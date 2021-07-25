using System.Threading.Tasks;
using atheneum_app.Library.DataAccess.Interfaces;
using atheneum_app.Library.Models.View;
using Refit;

namespace atheneum_app.Library.DataAccess.Implementations
{
    public class StatisticsService
    {
        private readonly IStatisticsService _statService;
        private static StatisticsService _instance;

        private StatisticsService()
        {
            var tokenClient = new TokenService();
            _statService = RestService.For<IStatisticsService>(Constants.V1BaseUrl, new RefitSettings
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(tokenClient.GetToken())
            });
        }

        public static StatisticsService Instance()
        {
            return _instance ??= new StatisticsService();
        }

        public Task<StatisticsViewModel> Get()
        {
            return _statService.Get();
        }
    }
}
