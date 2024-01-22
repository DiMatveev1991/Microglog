using BdService.DAL.Models;
using BdService.DAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdService.DAL.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogDbContext _context;

        public TagRepository(BlogDbContext context)
        {
            _context = context;
        }

        public HashSet<Tag> GetAllTags()
        {
            return _context.Tags.ToHashSet();
        }

        public Tag GetTag(Guid id)
        {
            return _context.Tags.FirstOrDefault(t => t.Id == id);
        }

        public async Task AddTag(Tag tag)
        {
            _context.Tags?.Add(tag);
            await SaveChangesAsync();
        }

        public async Task UpdateTag(Tag tag)
        {
            _context.Tags?.Update(tag);
            await SaveChangesAsync();
        }

        public async Task RemoveTag(Guid id)
        {
            _context.Tags?.Remove(GetTag(id));
            await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
