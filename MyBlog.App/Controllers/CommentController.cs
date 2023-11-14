using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels.Comments.Request;
using MyBlog.Data.Models.Articles;
using MyBlog.Data.Models.Users;
using NLog;
using NLog.Fluent;

namespace MyBlog.App.Controllers
{
    public class CommentController : Controller
    {
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();
		private readonly ICommentService _commentService;
        private readonly UserManager<User> _userManager;

        public CommentController(ICommentService commentService, UserManager<User> userManager)
        {
            _commentService = commentService;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("Comment/Create")]
        [Authorize]
        public IActionResult Create(int articleId)
        {
            
            var model = new CreateCommentViewModel() { ArticleId = articleId };
			Log.Info($"User - {User.Identity.Name}: Запрошена форма коментария к статье {articleId}.");

			return View(model);
        }

        [HttpPost]
        [Route("Comment/Create")]
        [Authorize]
        public async Task<IActionResult> CreateComment(CreateCommentViewModel model, int articleId)
        {
            model.ArticleId = articleId;
            var user = await _userManager.FindByNameAsync(User?.Identity?.Name);
            var comment = _commentService.CreateCommentAsync(model, user.Id);
			Log.Info($"User - {User.Identity.Name}: Добавлен коментарий к статье {articleId}.");

			return RedirectToAction("GetAll", "Article");
        }

        [Route("Comment/Edit")]
        [HttpGet]
        [Authorize(Roles = "Администратор, Модератор")]

        public async Task<IActionResult> Edit(int id)
        {
            var view = await _commentService.EditCommentAsync(id);
			Log.Info($"User - {User.Identity.Name}: Запрошена форма изменения коментария {id}.");

			return View(view);
        }

        [Route("Comment/Edit")]
        [HttpPost]
        [Authorize(Roles = "Администратор, Модератор")]
        public async Task<IActionResult> Edit(EditCommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _commentService.EditCommentAsync(model, model.Id);
				Log.Info($"User - {User.Identity.Name}: Коментарий изменен {model.Id}.");

				return RedirectToAction("GetAll", "Article");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
				Log.Error($"User - {User.Identity.Name}: Ошибка при изменении коментария {model.Id}.");

				return View(model);
            }
        }

        [HttpGet]
        [Route("Comment/Delete")]
        [Authorize(Roles = "Администратор, Модератор")]
        public async Task<IActionResult> Delete(int id, bool confirm = true)
        {
            if (confirm)
                await Delete(id);
			Log.Info($"User - {User.Identity.Name}: Коментарий удален {id}.");


			return RedirectToAction("GetAll", "Article");
        }

        [HttpDelete]
        [Route("Comment/Delete")]
        [Authorize(Roles = "Администратор, Модератор")]
        public async Task<IActionResult> Delete(int id)
        {
            await _commentService.DeleteCommentAsync(id);
			Log.Info($"User - {User.Identity.Name}: Коментарий удален {id}.");


			return RedirectToAction("GetAll", "Article");
        }
    }
}
