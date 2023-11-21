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
            var defaultUser = new RegisterUserViewModel { Login = "Administrator", Email = "admin@gmail.com", Password = "qwert", Firstname = "Administrator", Lastname = "Rootovich"};
            var defaultUser1 = new RegisterUserViewModel { Login = "Moderator", Email = "moderator@gmail.com", Password = "qwert", Firstname = "Moder", Lastname = "Banovich" };
            var defaultUser2 = new RegisterUserViewModel { Login = "User", Email = "user@gmail.com", Password = "qwert", Firstname = "Ivan", Lastname = "Ivanov" };

            var user = _mapper.Map<User>(defaultUser);
            var user1 = _mapper.Map<User>(defaultUser1);
            var user2 = _mapper.Map<User>(defaultUser2);

			var userRole = new Role() { Name = "Пользователь", Description = "Стандартный набор возможностей" };
			var moderRole = new Role() { Name = "Модератор", Description = "Расширенный набор возможностей - позволяет редактировать контент" };
            var adminRole = new Role() { Name = "Администратор", Description = "Роль с максимальным уровнем доступа" };

			if (_userService.Users.FirstOrDefault(x => x.UserName == "Moderator") == null)
            {
				await _userService.CreateAsync(user1, defaultUser1.Password);
				if (_roleService.Roles.FirstOrDefault(x => x.Name == "Модератор") == null)
					await _roleService.CreateAsync(moderRole);
				await _userService.AddToRoleAsync(user1, _roleService.Roles.FirstOrDefault(x => x.Name == "Модератор").Name);
			}

			if (_userService.Users.FirstOrDefault(x => x.UserName == "Administrator") == null)
            {
				await _userService.CreateAsync(user, defaultUser.Password);
				if (_roleService.Roles.FirstOrDefault(x => x.Name == "Администратор") == null)
					await _roleService.CreateAsync(adminRole);
				await _userService.AddToRoleAsync(user, _roleService.Roles.FirstOrDefault(x => x.Name == "Администратор").Name);
			}

			if (_userService.Users.FirstOrDefault(x => x.UserName == "User") == null)
            {
				await _userService.CreateAsync(user2, defaultUser2.Password);
				if (_roleService.Roles.FirstOrDefault(x => x.Name == "Пользователь") == null)
                    await _roleService.CreateAsync(userRole);
				await _userService.AddToRoleAsync(user2, _roleService.Roles.FirstOrDefault(x => x.Name == "Пользователь").Name);
			}
		}
    }
}
