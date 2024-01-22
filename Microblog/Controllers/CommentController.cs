using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLog;
using BdService.BLL.Services.IServices;
using BdService.BLL.ViewModels.Comments;
using BdService.DAL.Models;

namespace BdService.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<User> _userManager;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public CommentController(ICommentService commentService, UserManager<User> userManager)
        {
            _commentService = commentService;
            _userManager = userManager;
        }

        /// [Get] Метод, добавление комментария
        [HttpGet]
        [Route("Comment/CreateComment")]
        public IActionResult CreateComment(Guid postId)
        {
            var model = new CommentCreateViewModel() { PostId = postId };

            return View(model);
        }

        /// [Post] Метод, добавление комментария
        [HttpPost]
        [Route("Comment/CreateComment")]
        public async Task<IActionResult> CreateComment(CommentCreateViewModel model, Guid postId)
        {
            model.PostId = postId;
            var user = await _userManager.FindByNameAsync(User?.Identity?.Name);
            var post = _commentService.CreateComment(model, new Guid(user.Id));
            Logger.Info($"Пользователь {model.Author} добавил комментарий к статье {postId}");

            return RedirectToAction("GetPosts", "Post");
        }

        /// [Get] Метод, редактирования коментария
        [Route("Comment/Edit")]
        [HttpGet]
        public async Task<IActionResult> EditComment(Guid id)
        {
            var view = await _commentService.EditComment(id);

            return View(view);
        }

        /// [Post] Метод, редактирования коментария
        [Authorize]
        [Route("Comment/Edit")]
        [HttpPost]
        public async Task<IActionResult> EditComment(CommentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _commentService.EditComment(model, model.Id);
                Logger.Info($"Пользователь {model.Author} изменил комментарий {model.Id}");

                return RedirectToAction("GetPosts", "Post");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");

                return View(model);
            }
        }


        /// [Get] Метод, удаления коментария
        [HttpGet]
        [Route("Comment/Remove")]
        [Authorize(Roles = "Администратор, Модератор, Пользователь")]
        public async Task<IActionResult> RemoveComment(Guid id, bool confirm = true)
        {
            if (confirm)
                await RemoveComment(id);

            return RedirectToAction("GetPosts", "Post");
        }

        /// [Delete] Метод, удаления коментария
        [HttpDelete]
        [Route("Comment/Remove")]
        public async Task<IActionResult> RemoveComment(Guid id)
        {
            await _commentService.RemoveComment(id);
            Logger.Info($"Комментарий с id {id} удален");

            return RedirectToAction("GetPosts", "Post");
        }
    }
}
