using AutoMapper;
using Microsoft.AspNetCore.Identity;
using BdService.BLL.Services.IServices;
using BdService.BLL.ViewModels.Users;
using BdService.DAL.Models;

namespace BdService.BLL.Services
{
    public class HomeService : IHomeService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public IMapper _mapper;

        public HomeService(RoleManager<Role> roleManager, UserManager<User> userManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task GenerateData()
        {
            var testUser = new UserRegisterViewModel { UserName = "Admin", Email = "admin@gmail.com", Password = "123456", FirstName = "Admin", LastName = "Adminov" };
            var testUser2 = new UserRegisterViewModel { UserName = "Moder", Email = "moderator@gmail.com", Password = "123456", FirstName = "Moder", LastName = "Moderev" };
            var testUser3 = new UserRegisterViewModel { UserName = "User", Email = "user@gmail.com", Password = "123456", FirstName = "User", LastName = "Userov" };

            var user = _mapper.Map<User>(testUser);
            var user2 = _mapper.Map<User>(testUser2);
            var user3 = _mapper.Map<User>(testUser3);

            var userRole = new Role() { Name = "Пользователь", Description = "Стандартная роль приложения" };
            var moderRole = new Role() { Name = "Модератор", Description = "Данная роль позволяет выполнять редактирование, удаление комментариев и статей в приложении" };
            var adminRole = new Role() { Name = "Администратор", Description = "Роль с максимальными возможностями в приложении" };

            await _userManager.CreateAsync(user, testUser.Password);
            await _userManager.CreateAsync(user2, testUser2.Password);
            await _userManager.CreateAsync(user3, testUser3.Password);

            await _roleManager.CreateAsync(userRole);
            await _roleManager.CreateAsync(moderRole);
            await _roleManager.CreateAsync(adminRole);

            await _userManager.AddToRoleAsync(user, adminRole.Name);
            await _userManager.AddToRoleAsync(user2, moderRole.Name);
            await _userManager.AddToRoleAsync(user3, userRole.Name);
        }
    }
}
