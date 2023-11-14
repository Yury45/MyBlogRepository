using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels.Users.Request;
using MyBlog.Data.Models.Users;
using NLog;

namespace MyBlog.App.Controllers
{    /// <summary>
     /// Контроллер регистрации пользователя
     /// </summary>
    public class UserController : Controller
    {
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();
		private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController(SignInManager<User> signInManager, IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [Route("User/Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [Route("User/Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);

                if (result.Succeeded)
                {
					Log.Info($"Регистрация нового пользователя {model.Login}!");
					return RedirectToAction("Index", "Home");

                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
						Log.Error($"Ошибка при регистрации нового пользователя: {error.Description}!");

					}
				}
            }
            return View(model);
        }

        [Route("User/Login")]
        [HttpGet]
        public IActionResult Login()
        {
			Log.Info($"Запрошена форма входа.");

			return View();
        }

        [Route("User/Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);

                if (result.Succeeded)
                {
					Log.Info($"User --- {User.Identity.Name} выполнен вход в систему.");

					return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
					Log.Error($"Ошибка при входе в систему!");

				}
			}
            return View(model);
        }

		[Route("User/Logout")]
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await _userService.LogoutUserAccount();
			Log.Info($"User --- {User.Identity.Name} выполнен выход из системы.");

			return RedirectToAction("Index", "Home");
		}

		[Route("User/Create")]
        [Authorize(Roles = "Администратор")]
        [HttpGet]
		public IActionResult Create()
		{
			Log.Info($"User --- {User.Identity.Name}: Запрошена форма создания пользователя.");

			return View();
		}

		[Route("User/Create")]
        [Authorize(Roles = "Администратор")]
        [HttpPost]
		public async Task<IActionResult> Create(CreateUserViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _userService.CreateUserAsync(model);

				if (result.Succeeded)
				{
					Log.Info($"User --- {User.Identity.Name}: Пользователь {model.Login} создан.");

					return RedirectToAction("GetAll", "User");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
						Log.Error($"User --- {User.Identity.Name}: Ошибка при создании пользователя.");

					}
				}
			}
			return View(model);
		}

		[Route("User/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _userService.EditUserAsync(id);
			Log.Info($"User --- {User.Identity.Name}: Запрошена форма редактирования пользователя {id}.");

			return View(model);
        }

		[Route("User/Edit")]
		[Authorize(Roles = "Администратор, Модератор")]
		[HttpPost]
		public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _userService.EditUserAsync(model);
				Log.Info($"User --- {User.Identity.Name}: Пользователь {model.Id} обновлен.");

				return RedirectToAction("GetAll", "User");
            }
            else
            {
				Log.Error($"User --- {User.Identity.Name}: Ошибка при обновлении пользователя.");

				return View(model);
            }
        }

        [Route("User/GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetUsersAsync();
			Log.Info($"User --- {User.Identity.Name}: Запрошен список всех пользователей.");

			return View(users);
        }

        [Route("User/Get")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _userService.GetUserAsync(id);
			Log.Info($"User --- {User.Identity.Name}: Просмотрена информация пользователя {id}.");


			return View(model);
        }

        [Route("User/Delete")]
        [Authorize(Roles = "Администратор")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var account = await _userService.GetUserAsync(id);
            await _userService.DeleteUserAsync(id);
			Log.Info($"User --- {User.Identity.Name}: Пользователь {id} удален.");

			return RedirectToAction("GetAll", "User");
        }
    }
}
