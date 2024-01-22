using System.ComponentModel.DataAnnotations;

namespace BdService.BLL.ViewModels.Tags
{

    /// Модель изменения тега

    public class TagEditViewModel
    {
        public Guid Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Название")]
        public string? Name { get; set; }
    }
}
