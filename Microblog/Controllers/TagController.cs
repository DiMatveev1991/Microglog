using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BdService.BLL.Services.IServices;
using BdService.BLL.ViewModels.Tags;
using NLog;

namespace BdService.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagService _tagService;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }


        /// [Get] Метод, создания тега
        [Route("Tag/Create")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public IActionResult CreateTag()
        {
            return View();
        }


        /// [Post] Метод, создания тега
        [Route("Tag/Create")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> CreateTag(TagCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tagId = _tagService.CreateTag(model);
                Logger.Info($"Создан тег - {model.Name}");

                return RedirectToAction("GetTags", "Tag");
            }
            else
            {
                Logger.Error($"Ошибка при создании тега - {model.Name}");

                return View(model);
            }
        }


        /// [Get] Метод, редактирования тега
        [Route("Tag/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> EditTag(Guid id)
        {
            var view = await _tagService.EditTag(id);

            return View(view);
        }


        /// [Post] Метод, редактирования тега
        [Route("Tag/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> EditTag(TagEditViewModel model, Guid id)
        {
            if (ModelState.IsValid)
            {
                await _tagService.EditTag(model, id);
                Logger.Info($"Изменен тег - {model.Name}");

                return RedirectToAction("GetTags", "Tag");
            }
            else
            {
                Logger.Error($"Ошибка при изменении тега - {model.Name}");

                return View(model);
            }
        }


        /// [Get] Метод, удаления тега
        [Route("Tag/Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> RemoveTag(Guid id, bool isConfirm = true)
        {
            if (isConfirm)
                await RemoveTag(id);

            return RedirectToAction("GetTags", "Tag");
        }


        /// [Post] Метод, удаления тега
        [Route("Tag/Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> RemoveTag(Guid id)
        {
            var tag = await _tagService.GetTag(id);
            await _tagService.RemoveTag(id);
            Logger.Info($"Удаленн тег - {id}");

            return RedirectToAction("GetTags", "Tag");
        }


        /// [Get] Метод, получения всех тегов
        [Route("Tag/Get")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _tagService.GetTags();

            return View(tags);
        }

        
        /// [Get] Метод, просмотра данных о теге
        [Route("Tag/Details")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> DetailsTag(Guid id)
        {
            var tags = await _tagService.GetTag(id);

            return View(tags);
        }
    }
}
