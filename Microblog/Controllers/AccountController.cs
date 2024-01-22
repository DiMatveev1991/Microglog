using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BdService.BLL.Services.IServices;
using BdService.BLL.ViewModels.Users;
using NLog;

namespace SNBProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// [Get] Метод, login
        [Route("Account/Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        /// [Post] Метод, login
        [Route("Account/Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.Login(model);

                if (result.Succeeded)
                {
                    Logger.Info($"Осуществлен вход пользователя с адресом - {model.Email}");
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }


        /// [Get] Метод, создания пользователя
        [Route("Account/Create")]
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }


        /// [Post] Метод, создания пользователя
        [Route("Account/Create")]
        [HttpPost]
        public async Task<IActionResult> AddUser(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.CreateUser(model);

                if (result.Succeeded)
                {
                    Logger.Info($"Создан аккаунт, пользователем с правами администратора, с использованием адреса - {model.Email}");
                    return RedirectToAction("GetAccounts", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }


        /// [Get] Метод, регистрации
        [Route("Account/Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        /// [Post] Метод, регистрации
        [Route("Account/Register")]
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.Register(model);

                if (result.Succeeded)
                {
                    Logger.Info($"Создан аккаунт с использованием адреса - {model.Email}");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        /// [Get] Метод, редактирования аккаунта

        [Route("Account/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> EditAccount(Guid id)
        {
            var model = await _accountService.EditAccount(id);

            return View(model);
        }


        /// [Post] Метод, редактирования аккаунта
        [Route("Account/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> EditAccount(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _accountService.EditAccount(model);
                Logger.Info($"Аккаунт {model.UserName} был изменен");

                return RedirectToAction("GetAccounts", "Account");
            }

            else
            {
                return View(model);
            }
        }


        /// [Get] Метод, удаление аккаунта

        [Route("Account/Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> RemoveAccount(Guid id, bool confirm = true)
        {
            if (confirm)

                await RemoveAccount(id);

            return RedirectToAction("GetAccounts", "Account");
        }


        /// [Post] Метод, удаление аккаунта
        [Route("Account/Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> RemoveAccount(Guid id)
        {
            var account = await _accountService.GetAccount(id);
            await _accountService.RemoveAccount(id);
            Logger.Info($"Аккаунт с id - {id} удален");

            return RedirectToAction("GetAccounts", "Account");
        }


        /// [Post] Метод, выхода из аккаунта
        [Route("Account/Logout")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> LogoutAccount()
        {
            await _accountService.LogoutAccount();
            Logger.Info($"Осуществлен выход из аккаунта");

            return RedirectToAction("Index", "Home");
        }

        /// [Get] Метод, получения всех пользователей
        [Route("Account/Get")]
        [Authorize(Roles = "Администратор, Модератор")]
        public async Task<IActionResult> GetAccounts()
        {
            var users = await _accountService.GetAccounts();

            return View(users);
        }


        /// [Get] Метод, получения одного пользователя по Id
        [Route("Account/Details")]
        [Authorize(Roles = "Администратор, Модератор")]
        public async Task<IActionResult> DetailsAccount(Guid id)
        {
            var model = await _accountService.GetAccount(id);

            return View(model);
        }
    }
}
