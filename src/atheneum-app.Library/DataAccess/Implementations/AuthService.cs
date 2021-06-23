using System.Threading.Tasks;
using atheneum_app.Library.DataAccess.Interfaces;
using atheneum_app.Library.Models.Binding;
using atheneum_app.Library.Models.View;
using Refit;

namespace atheneum_app.Library.DataAccess.Implementations
{
    public sealed class AuthService
    {
        private readonly IAuthService _authService;

        public AuthService()
        {
            _authService = RestService.For<IAuthService>(Constants.V1BaseUrl);
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

        public Task<MessageViewModel> ForgotPassword(string emailAddress)
        {
            var bm = new ForgotPasswordBindingModel
            {
                EmailAddress = emailAddress
            };
            return _authService.ForgotPassword(bm);
        }

        public Task<MessageViewModel> ResetPassword(string emailAddress, string resetCode, string password)
        {
            var bm = new ResetPasswordBindingModel
            {
                EmailAddress = emailAddress,
                ResetCode = resetCode,
                Password = password
            };
            return _authService.ResettPassword(bm);
        }
    }
}