using System.Threading.Tasks;
using atheneum_app.Library.Models.Binding;
using atheneum_app.Library.Models.View;

namespace atheneum_app.Library.DataAccess.Interfaces
{
    public interface IAuthService
    {
        [Post("/auth/login")]
        Task<AuthViewModel> Login([Body] LoginBindingModel payload);

        [Post("/auth/register")]
        Task<AuthViewModel> Register([Body] RegisterBindingModel payload);
    }
}