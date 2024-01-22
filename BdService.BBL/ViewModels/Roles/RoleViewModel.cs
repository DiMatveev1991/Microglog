using System.ComponentModel.DataAnnotations;

namespace BdService.BLL.ViewModels.Roles
{

    /// Модель роли

    public class RoleViewModel
    {
        public string? Id { get; set; }

        [Display(Name = "Name")]
        public string? Name { get; set; }

        public bool IsSelected { get; set; }
    }
}
