using System.Collections.Generic;
using System.Threading.Tasks;
using AtheneumApp.Library.DataAccess.Interfaces;
using AtheneumApp.Library.Models.View;
using Refit;

namespace AtheneumApp.Library.DataAccess.Implementations
{
    public class PublisherService
    {
        private readonly IPublisherService _publisherService;
        private static PublisherService _instance;

        private PublisherService() =>
            _publisherService = RestService.For<IPublisherService>(Constants.V1BaseUrl, new RefitSettings
            {
                AuthorizationHeaderValueGetter = TokenService.GetAuthToken
            });

        public static PublisherService Instance() => _instance ?? (_instance = new PublisherService());

        public Task<IEnumerable<TopPublisherViewModel>> GetTop() => _publisherService.GetTop();
    }
}
