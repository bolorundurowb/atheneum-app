using System.Threading.Tasks;
using atheneum_app.Models.Binding;
using atheneum_app.Models.View;
using Refit;

namespace atheneum_app.DataAccess.Interfaces
{
    public interface IUserService
    {
        [Get("/users/current")]
        Task<UserViewModel> GetProfile();

        [Put("/users/current")]
        Task<UserViewModel> UpdateProfile([Body] UserProfileUpdateBindingModel payload);
    }
}