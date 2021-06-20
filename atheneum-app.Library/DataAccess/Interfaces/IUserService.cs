using System.Threading.Tasks;
using atheneum_app.Library.Models.Binding;
using atheneum_app.Library.Models.View;

namespace atheneum_app.Library.DataAccess.Interfaces
{
    public interface IUserService
    {
        [Get("/users/current")]
        [Headers("Authorization: Bearer")]
        Task<UserViewModel> GetProfile();

        [Put("/users/current")]
        [Headers("Authorization: Bearer")]
        Task<UserViewModel> UpdateProfile([Body] UserProfileUpdateBindingModel payload);
    }
}