using System.Threading.Tasks;
using AtheneumApp.Library.DataAccess.Interfaces;
using AtheneumApp.Library.Models.Binding;
using AtheneumApp.Library.Models.View;
using Refit;

namespace AtheneumApp.Library.DataAccess.Implementations
{
    public sealed class AuthService
    {
        private readonly IAuthService _authService;
        private static AuthService _instance;

        private AuthService()
        {
            _authService = RestService.For<IAuthService>(Constants.V1BaseUrl, new RefitSettings
            {
                AuthorizationHeaderValueGetter = TokenService.GetAuthToken
            });
        }

        public static AuthService Instance()
        {
            return _instance ??= new AuthService();
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

        public Task<AuthViewModel> Register(string fullName, string emailAddress, string password)
        {
            var bm = new RegisterBindingModel
            {
                FullName = fullName,
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
            return _authService.ResetPassword(bm);
        }

        public Task<MessageViewModel> VerifyEmail(string verificationCode)
        {
            var bm = new VerifyEmailBindingModel
            {
                VerificationCode = verificationCode
            };
            return _authService.VerifyEmail(bm);
        }

        public Task<MessageViewModel> ResendVerificationCode()
        {
            return _authService.ResendVerificationCode();
        }
    }
}
