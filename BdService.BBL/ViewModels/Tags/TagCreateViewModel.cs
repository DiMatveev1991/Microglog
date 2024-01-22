using System.ComponentModel.DataAnnotations;

namespace BdService.BLL.ViewModels.Tags
{

    /// Модель создания тега

    public class TagCreateViewModel
    {
        [Required(ErrorMessage = "Поле Название обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Название")]
        public string? Name { get; set; }
    }
}
