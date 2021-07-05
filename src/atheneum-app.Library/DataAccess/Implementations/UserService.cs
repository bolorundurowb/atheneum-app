using System.Threading.Tasks;
using atheneum_app.Library.DataAccess.Interfaces;
using atheneum_app.Library.Models.View;
using Refit;

namespace atheneum_app.Library.DataAccess.Implementations
{
    public sealed class UserService
    {
        private readonly IUserService _userService;

        public UserService()
        {
            _userService = RestService.For<IUserService>(Constants.V1BaseUrl);
        }

        public Task<UserViewModel> GetProfile()
        {
            return _userService.GetProfile();
        }
    }
}