using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels.Users.Request;
using MyBlog.Data.Models.Roles;
using MyBlog.Data.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.BLL.Services
{
    public class DataDefaultService : IDataDefaultService
    {
        private readonly RoleManager<Role> _roleService;
        private readonly UserManager<User> _userService;
        public IMapper _mapper;

        public DataDefaultService(RoleManager<Role> roleService, UserManager<User> userService, IMapper mapper)
        {
            _roleService = roleService;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task GenerateDefaultDate()
        {
            var defaultUser = new RegisterUserViewModel { Login = "Administrator", Email = "admin@gmail.com", Password = "qwert", Firstname = "Admin", Lastname = "Rootovich" };
            var defaultUser1 = new RegisterUserViewModel { Login = "Moderator", Email = "moderator@gmail.com", Password = "qwert", Firstname = "Moder", Lastname = "Banovich" };
            var defaultUser2 = new RegisterUserViewModel { Login = "User", Email = "user@gmail.com", Password = "qwert", Firstname = "Ivan", Lastname = "Ivanov" };

            var user = _mapper.Map<User>(defaultUser);
            var user2 = _mapper.Map<User>(defaultUser1);
            var user3 = _mapper.Map<User>(defaultUser2);

            var userRole = new Role() { Name = "Пользователь", Description = "Стандартный набор возможностей" };
            var moderRole = new Role() { Name = "Модератор", Description = "Расширенный набор возможностей - позволяет редактировать контент" };
            var adminRole = new Role() { Name = "Администратор", Description = "Роль с максимальным уровнем доступа" };

            await _userService.CreateAsync(user, defaultUser.Password);
            await _userService.CreateAsync(user2, defaultUser1.Password);
            await _userService.CreateAsync(user3, defaultUser2.Password);

            await _roleService.CreateAsync(userRole);
            await _roleService.CreateAsync(moderRole);
            await _roleService.CreateAsync(adminRole);

            await _userService.AddToRoleAsync(user, adminRole.Name);
            await _userService.AddToRoleAsync(user2, moderRole.Name);
            await _userService.AddToRoleAsync(user3, userRole.Name);
        }
    }
}
