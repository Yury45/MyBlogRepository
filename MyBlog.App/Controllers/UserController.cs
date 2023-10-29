using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels.Users.Request;
using MyBlog.Data.Models.Users;

namespace MyBlog.App.Controllers
{    /// <summary>
     /// Контроллер регистрации пользователя
     /// </summary>
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController(SignInManager<User> signInManager, IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [Route("Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);

                if (result.Succeeded)
                {
                    //return RedirectToAction("GetAccounts", "Account");
                    Console.WriteLine($"Осуществлена регистрация пользователя с адресом - {model.Email}");
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

        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);

                if (result.Succeeded)
                {
                    Console.WriteLine($"Осуществлен вход пользователя с адресом - {model.Email}");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

		[Route("Create")]
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[Route("Create")]
		[HttpPost]
		public async Task<IActionResult> Create(CreateUserViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _userService.CreateUserAsync(model);

				if (result.Succeeded)
				{
					return RedirectToAction("GetUsers", "User");
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

		[Route("Edit")]
        //[Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> EditUser(int id)
        {
            var model = await _userService.EditUserAsync(id);

            return View(model);
        }

		[Route("Edit")]
		//[Authorize(Roles = "Администратор, Модератор")]
		[HttpPost]
		public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _userService.EditUserAsync(model);

                return RedirectToAction("GetUsers", "User");
            }
            else
            {
                return View(model);
            }
        }

        [Route("GetUsers")]
        [HttpGet]
        //[Authorize(Roles = "Администратор, Модератор")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync();

            return View(users);
        }

        [Route("GetUser")]
        //[Authorize(Roles = "Администратор, Модератор")]
        public async Task<IActionResult> GetUser(int id)
        {
            var model = await _userService.GetUserAsync(id);

            return View(model);
        }

    }
}
