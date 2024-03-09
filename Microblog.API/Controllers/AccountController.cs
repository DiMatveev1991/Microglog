using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BdService.BLL.Services.IServices;
using BdService.BLL.ViewModels.Users;
using BdService.DAL.Models;

namespace Microblog.API.Controllers
{
    [ApiController]
    [Route("controller")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<User> _userManager;

        public AccountController(IAccountService accountService, UserManager<User> userManager)
        {
            _accountService = accountService;
            _userManager = userManager;
        }

        /// <summary>
        /// Авторизация аккаунта пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///        "email": "test@gmail.com", 
        ///        "password": "test123"
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                throw new ArgumentNullException("Некорректный запрос");

            var result = await _accountService.Login(model);
            if (!result.Succeeded)
                throw new AuthenticationException("Введен неверный пароль или такого аккаунта не существует");

            var user = await _userManager.FindByEmailAsync(model.Email);
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            if (roles.Contains("Администратор"))
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, "Администратор"));
            else
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, roles.First()));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            return StatusCode(200);
        }

        /// <summary>
        /// Добавление аккаунта пользователя
        /// </summary>
        /// <remarks>
        /// 
        /// Для добавления аккаунта пользователя необходимы права администратора
        /// 
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///        "firstName": "Test",
        ///        "lastName": "Testov",
        ///        "userName": "Tester",
        ///        "email": "test@gmail.com", 
        ///        "password": "test123",
        ///        "passwordReg": "test123"
        ///     }
        ///
        /// </remarks>
        [Authorize(Roles = "Администратор")]
        [HttpPost]
        [Route("AddAccount")]
        public async Task<IActionResult> AddAccount(UserRegisterViewModel model)
        {
            var result = await _accountService.Register(model);

            return StatusCode(result.Succeeded ? 201 : 204);
        }

        /// <summary>
        /// Редактирование аккаунта пользователя
        /// </summary>
        /// <remarks>
        /// Для редактирования данных пользователя необходимы права администратора
        /// </remarks>
        [Authorize(Roles = "Администратор")]
        [HttpPatch]
        [Route("EditAccount")]
        public async Task<IActionResult> EditAccount(UserEditViewModel model)
        {
            var result = await _accountService.EditAccount(model);

            return StatusCode(result.Succeeded ? 201 : 204);
        }

        /// <summary>
        /// Удаление аккаунта пользователя
        /// </summary>
        /// <remarks>
        /// Для удаления пользователя необходимы права администратора
        /// </remarks>
        [Authorize(Roles = "Администратор")]
        [HttpDelete]
        [Route("RemoveAccount")]
        public async Task<IActionResult> RemoveAccount(Guid id)
        {
            await _accountService.RemoveAccount(id);

            return StatusCode(201);
        }

        /// <summary>
        /// Получение всех аккаунтов пользователей
        /// </summary>
        /// <remarks>
        /// Для получения всех аккаунтов пользователей необходимы права администратора
        /// </remarks>
        [Authorize(Roles = "Администратор")]
        [HttpGet]
        [Route("GetAccounts")]
        public Task<List<User>> GetAccounts()
        {
            var users = _accountService.GetAccounts();

            return Task.FromResult(users.Result);
        }
    }
}
