using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLog;
using BdService.BLL.Services.IServices;
using BdService.BLL.ViewModels.Posts;
using BdService.DAL.Models;

namespace BdService.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly UserManager<User> _userManager;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostController(IPostService postService, UserManager<User> userManager)
        {
            _postService = postService;
            _userManager = userManager;
        }


        /// [Get] Метод, показывания поста
        [Route("Post/Show")]
        [HttpGet]
        public async Task<IActionResult> ShowPost(Guid id)
        {
            var post = await _postService.ShowPost(id);

            return View(post);
        }


        /// [Get] Метод, создания поста
        [Route("Post/Create")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreatePost()
        {
            var model = await _postService.CreatePost();

            return View(model);
        }


        /// [Post] Метод, создания поста
        [Route("Post/Create")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost(PostCreateViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User?.Identity?.Name);
            model.AuthorId = user.Id;
            if (string.IsNullOrEmpty(model.Title) || string.IsNullOrEmpty(model.Content))
            {
                ModelState.AddModelError("", "Не все поля заполненны");
                Logger.Error($"Пост не создан, ошибка при создании - Не все поля заполнены");

                return View(model);
            }
            await _postService.CreatePost(model);
            Logger.Info($"Создан пост - {model.Title}");

            return RedirectToAction("GetPosts", "Post");
        }


        /// [Get] Метод, редактирования поста
        [Route("Post/Edit")]
        [HttpGet]
        public async Task<IActionResult> EditPost(Guid id)
        {
            var model = await _postService.EditPost(id);

            return View(model);
        }


        /// [Post] Метод, редактирования поста
        [Authorize]
        [Route("Post/Edit")]
        [HttpPost]
        public async Task<IActionResult> EditPost(PostEditViewModel model, Guid Id)
        {
            if (string.IsNullOrEmpty(model.Title) || string.IsNullOrEmpty(model.Content))
            {
                ModelState.AddModelError("", "Не все поля заполненны");
                Logger.Error($"Пост не отредактирован, ошибка при редактировании - Не все поля заполнены");

                return View(model);
            }
            await _postService.EditPost(model, Id);
            Logger.Info($"Пост {model.Title} отредактирован");

            return RedirectToAction("GetPosts", "Post");
        }


        /// [Get] Метод, удаления поста
        [HttpGet]
        [Route("Post/Remove")]
        public async Task<IActionResult> RemovePost(Guid id, bool confirm = true)
        {
            if (confirm)
                await RemovePost(id);

            return RedirectToAction("GetPosts", "Post");
        }


        /// [Post] Метод, удаления поста
        [HttpPost]
        [Route("Post/Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        public async Task<IActionResult> RemovePost(Guid id)
        {
            await _postService.RemovePost(id);
            Logger.Info($"Пост с id {id} удален");

            return RedirectToAction("GetPosts", "Post");
        }


        /// [Get] Метод, получения всех постов
        [HttpGet]
        [Route("Post/Get")]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postService.GetPosts();

            return View(posts);
        }
    }
}

