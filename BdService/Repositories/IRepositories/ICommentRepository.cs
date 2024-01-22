using BdService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdService.DAL.Repositories.IRepositories
{
    public interface ICommentRepository
    {
        List<Comment> GetAllComments();

        Comment GetComment(Guid id);

        List<Comment> GetCommentsByPostId(Guid id);

        Task AddComment(Comment item);

        Task UpdateComment(Comment item);

        Task RemoveComment(Guid id);

        Task<bool> SaveChangesAsync();
    }
}
