using BdService.DAL.Models;
using BdService.BLL.ViewModels.Roles;

namespace BdService.BLL.Services.IServices
{
    public interface IRoleService
    {
        Task<Guid> CreateRole(RoleCreateViewModel model);

        Task EditRole(RoleEditViewModel model);

        Task RemoveRole(Guid id);

        Task<List<Role>> GetRoles();

        Task<Role?> GetRole(Guid id);
    }
}
