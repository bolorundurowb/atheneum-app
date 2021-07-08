using System.Threading.Tasks;
using atheneum_app.Library.DataAccess.Interfaces;
using atheneum_app.Library.Models.Binding;
using atheneum_app.Library.Models.View;
using Refit;

namespace atheneum_app.Library.DataAccess.Implementations
{
    public sealed class UserService
    {
        private readonly IUserService _userService;

        public UserService()
        {
            var tokenClient = new TokenService();
            _userService = RestService.For<IUserService>(Constants.V1BaseUrl, new RefitSettings()
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(tokenClient.GetToken())
            });
        }

        public Task<UserViewModel> GetProfile()
        {
            return _userService.GetProfile();
        }

        public Task<UserViewModel> UpdateProfile(string firstName, string lastName)
        {
            var bm = new UserProfileUpdateBindingModel
            {
                FirstName = firstName,
                LastName = lastName
            };

            return _userService.UpdateProfile(bm);
        }

        public Task<MessageViewModel> UpdatePassword(string currentPassword, string newPassword)
        {
            var bm = new PasswordUpdateBindingModel
            {
                CurrentPassword = currentPassword,
                NewPassword = newPassword
            };

            return _userService.UpdatePassword(bm);
        }
    }
}