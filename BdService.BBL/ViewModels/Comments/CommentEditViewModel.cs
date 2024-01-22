using System.ComponentModel.DataAnnotations;

namespace BdService.BLL.ViewModels.Comments
{

    /// Модель изменения комментария

    public class CommentEditViewModel
    {

        /// Содержание комментария

        [DataType(DataType.Text)]
        [Display(Name = "Содержание", Prompt = "Содержание")]
        public string? Content { get; set; }

 
        /// Автор комментария

        [DataType(DataType.Text)]
        [Display(Name = "Автор", Prompt = "Автор")]
        public string? Author { get; set; }


        /// Id комментария

        public Guid Id { get; set; }
    }
}
