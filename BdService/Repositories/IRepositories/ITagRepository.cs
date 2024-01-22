using BdService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdService.DAL.Repositories.IRepositories
{
    public interface ITagRepository
    {
        HashSet<Tag> GetAllTags();

        Tag GetTag(Guid id);

        Task AddTag(Tag tag);

        Task UpdateTag(Tag tag);

        Task RemoveTag(Guid id);

        Task<bool> SaveChangesAsync();
    }
}
