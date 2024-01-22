using BdService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdService.DAL.Repositories.IRepositories
{
    public interface IPostRepository
    {
        List<Post> GetAllPosts();

        Post GetPost(Guid id);

        Task AddPost(Post post);

        Task UpdatePost(Post post);

        Task RemovePost(Guid id);

        Task<bool> SaveChangesAsync();
    }
}
