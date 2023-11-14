using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels.Articles.Request;
using MyBlog.Data.Models.Articles;
using MyBlog.Data.Models.Users;
using NLog;

namespace MyBlog.App.Controllers
{
	public class ArticleController : Controller
	{
		private readonly IArticleService _articleService;
		private readonly UserManager<User> _userService;
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();

		public ArticleController(IArticleService articleService, UserManager<User> userService)
		{
			_articleService = articleService;
			_userService = userService;
		}

		[Route("Article/Get")]
		[HttpGet]
		public async Task<IActionResult> Get(int id)
		{
			var article = await _articleService.GetArticleAsync(id);
			Log.Info($"User - {User.Identity.Name}: Запрошена статья {id}.");
			return View(article);
		}

		[Route("Article/Create")]
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Create()
		{
			var model = await _articleService.CreateArticleAsync();
			Log.Info($"User - {User.Identity.Name}: Запрошена форма для создания статьи.");


			return View(model);
		}

		[Route("Article/Create")]
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Create(CreateArticleViewModel model)
		{
            var user = await _userService.FindByNameAsync(User?.Identity?.Name);
			model.AuthorId = user.Id;
			if (string.IsNullOrEmpty(model.Title) || string.IsNullOrEmpty(model.Content))
			{
				ModelState.AddModelError("", "Напишите название и тело статьи");
				Log.Error($"User - {User.Identity.Name}: Ошибка при создании статьи - не все обязательные поля заполнены!");

				return View(model);
			}
			await _articleService.CreateArticleAsync(model);
			Log.Info($"User - {User.Identity.Name}: Создана статья {model.Title} - {model.Id}!");

			return RedirectToAction("GetAll", "Article");
		}

		[Route("Article/Edit")]
		[HttpGet]
        [Authorize(Roles = "Администратор, Модератор")]
        public async Task<IActionResult> Edit(int id)
		{
			var model = await _articleService.UpdateArticleAsync(id);
			Log.Info($"User - {User.Identity.Name}: Запрошена форма для редактирования статьи.");

			return View(model);
		}

		[Route("Article/Edit")]
		[HttpPost]
        [Authorize(Roles = "Администратор, Модератор")]
        public async Task<IActionResult> Edit(EditArticleViewModel model, int Id)
		{
			if (string.IsNullOrEmpty(model.Title) || string.IsNullOrEmpty(model.Content))
			{
				ModelState.AddModelError("", "Не все поля заполненны");
				Log.Error($"User - {User.Identity.Name}: Ошибка при редактировании статьи - не все обязательные поля заполнены!");

				return View(model);
			}
			await _articleService.UpdateArticleAsync(model, Id);
			Log.Info($"User - {User.Identity.Name}: Изменена статья {model.Title} - {model.Id}!");

			return RedirectToAction("GetAll", "Article");
		}

        [HttpGet]
		[Route("Article/Delete")]
        [Authorize(Roles = "Администратор, Модератор")]
        public async Task<IActionResult> Delete(int id, bool confirm = true)
		{
			if (confirm)
				await Delete(id);
			Log.Info($"User - {User.Identity.Name}: Удалена статья  - {id}!");

			return RedirectToAction("GetAll", "Article");
		}

		[HttpPost]
		[Route("Article/Delete")]
        [Authorize(Roles = "Администратор, Модератор")]
        public async Task<IActionResult> Delete(int id)
		{
			await _articleService.DeleteArticleAsync(id);
			Log.Info($"User - {User.Identity.Name}: Удалена статья  - {id}!");

			return RedirectToAction("GetAll", "Article");
		}

		[HttpGet]
		[Route("Article/GetAll")]
		public async Task<IActionResult> GetAll()
		{
			var articles = await _articleService.GetAllArticlesAsync();
			Log.Info($"User - {User.Identity.Name}: Запрошен список статей.");

			return View(articles);
		}
	}
}
