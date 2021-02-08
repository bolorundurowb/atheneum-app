using System.Threading.Tasks;
using atheneum_app.Models.Binding;
using atheneum_app.Models.View;
using Refit;

namespace atheneum_app.DataAccess.Interfaces
{
    public interface IAuthService
    {
        [Post("/auth/login")]
        Task<AuthViewModel> Login([Body] LoginBindingModel payload);

        [Post("/auth/register")]
        Task<AuthViewModel> Register([Body] RegisterBindingModel payload);
    }
}