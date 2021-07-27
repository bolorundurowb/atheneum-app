using System.Threading.Tasks;
using atheneum_app.Library.Models.Binding;
using atheneum_app.Library.Models.View;
using Refit;

namespace atheneum_app.Library.DataAccess.Interfaces
{
    public interface IAuthService
    {
        [Post("/auth/login")]
        Task<AuthViewModel> Login([Body] LoginBindingModel payload);

        [Post("/auth/register")]
        Task<AuthViewModel> Register([Body] RegisterBindingModel payload);

        [Post("/auth/forgot-password")]
        Task<MessageViewModel> ForgotPassword([Body] ForgotPasswordBindingModel payload);

        [Post("/auth/reset-password")]
        Task<MessageViewModel> ResetPassword([Body] ResetPasswordBindingModel payload);

        [Post("/auth/verify-email")]
        Task<MessageViewModel> VerifyEmail([Body] VerifyEmailBindingModel payload);
    }
}
