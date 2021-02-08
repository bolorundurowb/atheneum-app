using System.Threading.Tasks;
using atheneum_app.DataAccess.Interfaces;
using atheneum_app.Models.Binding;
using atheneum_app.Models.View;
using Refit;

namespace atheneum_app.DataAccess.Implementations
{
    public sealed class AuthService
    {
        private readonly IAuthService _authService;

        public AuthService()
        {
            _authService = RestService.For<IAuthService>(Constants.BaseUrl);
        }

        public Task<AuthViewModel> Login(string emailAddress, string password)
        {
            var bm = new LoginBindingModel
            {
                EmailAddress = emailAddress,
                Password = password
            };
            return _authService.Login(bm);
        }

        public Task<AuthViewModel> Register(string emailAddress, string password)
        {
            var bm = new RegisterBindingModel
            {
                EmailAddress = emailAddress,
                Password = password
            };
            return _authService.Register(bm);
        }
    }
}