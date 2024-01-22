using System.ComponentModel.DataAnnotations;

namespace BdService.BLL.ViewModels.Comments
{

    /// Модель создания комментария

    public class CommentCreateViewModel
    {

        /// Содержание комментария
 
        [Required(ErrorMessage = "Поле Содержание обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Содержание", Prompt = "Содержание")]
        public string? Content { get; set; }


        /// Автор комментария

        [Required(ErrorMessage = "Поле Автор обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Автор", Prompt = "Автор")]
        public string? Author { get; set; }


        /// Id поста для которого создается комментарий

        public Guid PostId;
    }
}
