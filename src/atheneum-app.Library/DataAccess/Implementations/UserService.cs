using System.Threading.Tasks;
using AtheneumApp.Library.DataAccess.Interfaces;
using AtheneumApp.Library.Models.Binding;
using AtheneumApp.Library.Models.View;
using Refit;

namespace AtheneumApp.Library.DataAccess.Implementations
{
    public sealed class UserService
    {
        private readonly IUserService _userService;

        public UserService() =>
            _userService = RestService.For<IUserService>(Constants.V1BaseUrl, new RefitSettings
            {
                AuthorizationHeaderValueGetter = TokenService.GetAuthToken
            });

        public Task<UserViewModel> GetProfile() => _userService.GetProfile();

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
