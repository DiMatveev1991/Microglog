using AutoMapper;
using BdService.BLL.Services.IServices;
using BdService.BLL.ViewModels.Comments;
using BdService.DAL.Models;
using BdService.DAL.Repositories.IRepositories;

namespace BdService.BLL.Services
{
    public class CommentService : ICommentService
    {
        public IMapper _mapper;
        private readonly ICommentRepository _commentRepo;

        public CommentService(IMapper mapper, ICommentRepository commentRepo)
        {
            _mapper = mapper;
            _commentRepo = commentRepo;
        }

        public async Task<Guid> CreateComment(CommentCreateViewModel model, Guid userId)
        {
            var comment = new Comment
            {
                Content = model.Content,
                AuthorName = model.Author,
                PostId = model.PostId,
            };
            await _commentRepo.AddComment(comment);
            return comment.Id;
        }

        public async Task<CommentEditViewModel> EditComment(Guid id)
        {
            var comment = _commentRepo.GetComment(id);
            var result = new CommentEditViewModel
            {
                Content = comment.Content,
                Author = comment.AuthorName,
            };
            return result;
        }

        public async Task EditComment(CommentEditViewModel model, Guid id)
        {
            var comment = _commentRepo.GetComment(id);
            comment.Content = model.Content;
            comment.AuthorName = model.Author;
            await _commentRepo.UpdateComment(comment);
        }

        public async Task RemoveComment(Guid id)
        {
            await _commentRepo.RemoveComment(id);
        }

        public async Task<List<Comment>> GetComments()
        {
            return _commentRepo.GetAllComments().ToList();
        }
    }
}
