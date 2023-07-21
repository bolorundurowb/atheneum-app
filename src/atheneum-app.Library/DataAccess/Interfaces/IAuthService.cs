using System.Threading.Tasks;
using AtheneumApp.Library.Models.Binding;
using AtheneumApp.Library.Models.View;
using Refit;

namespace AtheneumApp.Library.DataAccess.Interfaces
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
        [Headers("Authorization: Bearer")]
        Task<MessageViewModel> VerifyEmail([Body] VerifyEmailBindingModel payload);

        [Post("/auth/resend-verification-code")]
        [Headers("Authorization: Bearer")]
        Task<MessageViewModel> ResendVerificationCode();
    }
}
