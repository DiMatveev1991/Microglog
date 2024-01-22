using BdService.DAL.Models;
using BdService.BLL.ViewModels.Tags;

namespace BdService.BLL.Services.IServices
{
    public interface ITagService
    {
        Task<Guid> CreateTag(TagCreateViewModel model);

        Task<TagEditViewModel> EditTag(Guid id);

        Task EditTag(TagEditViewModel model, Guid id);

        Task RemoveTag(Guid id);

        Task<List<Tag>> GetTags();

        Task<Tag> GetTag(Guid id);
    }
}
