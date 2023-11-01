using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels.Comments.Request;
using MyBlog.Data.Models.Users;

namespace MyBlog.App.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<User> _userManager;

        public CommentController(ICommentService commentService, UserManager<User> userManager)
        {
            _commentService = commentService;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("Comment/Create")]
        public IActionResult Create(int articleId)
        {
            var model = new CreateCommentViewModel() { ArticleId = articleId };

            return View(model);
        }

        [HttpPost]
        [Route("Comment/Create")]
        public async Task<IActionResult> CreateComment(CreateCommentViewModel model, int articleId)
        {
            model.ArticleId = articleId;
            var user = await _userManager.FindByNameAsync(User?.Identity?.Name);
            var comment = _commentService.CreateCommentAsync(model, user.Id);

            return RedirectToAction("GetAll", "Article");
        }

        [Route("Comment/Edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var view = await _commentService.EditCommentAsync(id);

            return View(view);
        }

        [Authorize]
        [Route("Comment/Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditCommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _commentService.EditCommentAsync(model, model.Id);

                return RedirectToAction("GetAll", "Article");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");

                return View(model);
            }
        }

        [HttpGet]
        [Route("Comment/Delete")]
        [Authorize(Roles = "Администратор, Модератор, Пользователь")]
        public async Task<IActionResult> Delete(int id, bool confirm = true)
        {
            if (confirm)
                await Delete(id);

            return RedirectToAction("GetAll", "Article");
        }

        [HttpDelete]
        [Route("Comment/Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _commentService.DeleteCommentAsync(id);

            return RedirectToAction("GetAll", "Article");
        }
    }
}
