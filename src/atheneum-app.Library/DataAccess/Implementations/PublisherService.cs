using System.Collections.Generic;
using System.Threading.Tasks;
using atheneum_app.Library.DataAccess.Interfaces;
using atheneum_app.Library.Models.Binding;
using atheneum_app.Library.Models.View;
using Refit;

namespace atheneum_app.Library.DataAccess.Implementations
{
    public class PublisherService
    {
        private readonly IPublisherService _publisherService;

        public PublisherService()
        {
            var tokenClient = new TokenService();
            _publisherService = RestService.For<IPublisherService>(Constants.V1BaseUrl, new RefitSettings
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(tokenClient.GetToken())
            });
        }

        public Task<IEnumerable<TopPublisherViewModel>> GetTop()
        {
            return _publisherService.GetTop();
        }
    }
}