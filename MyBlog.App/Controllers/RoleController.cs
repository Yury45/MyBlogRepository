using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels.Roles.Request;
using NLog;
using NLog.Fluent;

namespace MyBlog.App.Controllers
{
	public class RoleController : Controller
	{
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();
		private readonly IRoleService _roleService;

		public RoleController(IRoleService roleService)
		{
			_roleService = roleService;
		}

		[Route("Role/Create")]
		[Authorize(Roles = "Администратор, Модератор")]
		[HttpGet]
		public IActionResult Create()
		{
			Log.Info($"User - {User.Identity.Name}: Запрошена форма создания Роли.");

			return View();
		}

		[Route("Role/Create")]
		[Authorize(Roles = "Администратор, Модератор")]
		[HttpPost]
		public async Task<IActionResult> Create(CreateRoleViewModel model)
		{
			if (ModelState.IsValid)
			{
				var roleId = await _roleService.CreateRoleAsync(model);
				Log.Info($"User - {User.Identity.Name}: Добавлена новая Роль {model.Name}.");


				return RedirectToAction("GetAll", "Role");
			}
			else
			{
				ModelState.AddModelError("", "Некорректные данные");
				Log.Error($"User - {User.Identity.Name}: Ошибка при добавлении Роли {model.Name}!");


				return View(model);
			}
		}

		[Route("Role/Edit")]
		[Authorize(Roles = "Администратор, Модератор")]
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var role = _roleService.GetRoleAsync(id);
			var view = new EditRoleViewModel { Id = id, Description = role.Result?.Description, Name = role.Result?.Name };
			Log.Info($"User - {User.Identity.Name}: Запрошена форма изменения Роли {id}.");


			return View(view);
		}

		[Route("Role/Edit")]
		[Authorize(Roles = "Администратор, Модератор")]
		[HttpPost]
		public async Task<IActionResult> Edit(EditRoleViewModel model)
		{
			if (ModelState.IsValid)
			{
				await _roleService.EditRoleAsync(model);
				Log.Info($"User - {User.Identity.Name}: Изменена Роль {model.Name}.");

				return RedirectToAction("GetAll", "Role");
			}
			else
			{
				ModelState.AddModelError("", "Некорректные данные");
				Log.Error($"User - {User.Identity.Name}: Ошибка при изменении Роли {model.Name}!");


				return View(model);
			}
		}

		[Route("Role/Delete")]
		[Authorize(Roles = "Администратор, Модератор")]
		[HttpGet]
		public async Task<IActionResult> Delete(int id, bool isConfirm = true)
		{
			if (isConfirm)
				await Delete(id);
			Log.Info($"User - {User.Identity.Name}: Удалена Роль {id}.");

			return RedirectToAction("GetAll", "Role");
		}

		[Route("Role/Delete")]
		[Authorize(Roles = "Администратор, Модератор")]
		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			await _roleService.DeleteRoleAsync(id);
			Log.Info($"User - {User.Identity.Name}: Удалена Роль {id}.");

			return RedirectToAction("GetAll", "Role");
		}

		[Route("Role/GetAll")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var roles = await _roleService.GetRolesAsync();
			Log.Info($"User - {User.Identity.Name}: Запрошен список ролей.");
			return View(roles);
		}
	}
}
