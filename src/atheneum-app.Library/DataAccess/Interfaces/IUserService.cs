using System.Threading.Tasks;
using AtheneumApp.Library.Models.Binding;
using AtheneumApp.Library.Models.View;
using Refit;

namespace AtheneumApp.Library.DataAccess.Interfaces
{
    public interface IUserService
    {
        [Get("/users/current")]
        [Headers("Authorization: Bearer")]
        Task<UserViewModel> GetProfile();

        [Put("/users/current")]
        [Headers("Authorization: Bearer")]
        Task<UserViewModel> UpdateProfile([Body] UserProfileUpdateBindingModel payload);

        [Put("/users/current/passwords")]
        [Headers("Authorization: Bearer")]
        Task<MessageViewModel> UpdatePassword([Body] PasswordUpdateBindingModel payload);
    }
}