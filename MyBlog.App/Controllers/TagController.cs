using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels.Tags.Request;
using NLog;
using NLog.Fluent;

namespace MyBlog.App.Controllers
{
    public class TagController : Controller
    {
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();
		private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [Route("Tag/Create")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public IActionResult Create()
        {
			Log.Info($"User - {User.Identity.Name}: Запрошена форма создания тега.");

			return View();
        }

        [Route("Tag/Create")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateTagViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tagId = _tagService.CreateTagAsync(model);
				Log.Info($"User - {User.Identity.Name}: Создан новый тег {model.Name}");


				return RedirectToAction("GetAll", "User");
            }
            else
            {
				Log.Error($"User - {User.Identity.Name}: Ошибка при создании тега.");

				return View(model);
            }
        }

        [Route("Tag/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var view = await _tagService.UpdateTagAsync(id);
			Log.Info($"User - {User.Identity.Name}: Запрос на изменения тега {id}.");


			return View(view);
        }

        [Route("Tag/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                await _tagService.UpdateTagAsync(model, id);
				Log.Info($"User - {User.Identity.Name}: Тег {id} обновлен.");

				return RedirectToAction("GetAll", "Tag");
            }
            else
            {
				Log.Error($"User - {User.Identity.Name}: Ошибка при обновлении тега.");

				return View(model);
            }
        }

        [Route("Tag/Delete")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id, bool isConfirm = true)
        {
            if (isConfirm)
                await Delete(id);
			Log.Info($"User - {User.Identity.Name}: Тег {id} удален.");

			return RedirectToAction("GetAll", "Tag");
        }

        [Route("Tag/Delete")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var tag = await _tagService.GetTagByIdAsync(id);
            await _tagService.DeleteTagAsync(tag);
			Log.Info($"User - {User.Identity.Name}: Тег {id} удален.");

			return RedirectToAction("GetAll", "Tag");
        }

        [Route("Tag/GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _tagService.GetAllTagsAsync();
			Log.Info($"User - {User.Identity.Name}: Запрошен список тегов.");


			return View(tags);
        }

        [Route("Tag/Details")]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var tags = await _tagService.GetTagByIdAsync(id);
			Log.Info($"User - {User.Identity.Name}: Запрошена информация по тегу {id}.");


			return View(tags);
        }
    }
}
