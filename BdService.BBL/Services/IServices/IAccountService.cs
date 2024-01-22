using Microsoft.AspNetCore.Identity;
using BdService.DAL.Models;
using BdService.BLL.ViewModels.Users;

namespace BdService.BLL.Services.IServices
{
    public interface IAccountService
    {
        Task<IdentityResult> Register(UserRegisterViewModel model);

        Task<IdentityResult> CreateUser(UserCreateViewModel model);

        Task<SignInResult> Login(UserLoginViewModel model);

        Task<IdentityResult> EditAccount(UserEditViewModel model);

        Task<UserEditViewModel> EditAccount(Guid id);

        Task RemoveAccount(Guid id);

        Task<List<User>> GetAccounts();

        Task<User> GetAccount(Guid id);

        Task LogoutAccount();
    }
}
