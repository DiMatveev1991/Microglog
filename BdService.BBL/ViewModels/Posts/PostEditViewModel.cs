using BdService.BLL.ViewModels.Tags;
using System.ComponentModel.DataAnnotations;

namespace BdService.BLL.ViewModels.Posts
{
    /// <summary>
    /// Модель изменения поста
    /// </summary>
    public class PostEditViewModel
    {
        public Guid Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Заголовок", Prompt = "Заголовок")]
        public string? Title { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Содержание", Prompt = "Содержание")]
        public string? Content { get; set; }

        [Display(Name = "Теги", Prompt = "Теги")]
        public List<TagViewModel>? Tags { get; set; }
    }
}
