using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels;
using MyBlog.Data.Models.Users;
using MyBlog.Data.Repositories.Interfaces;
using System.Diagnostics;


namespace MyBlog.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataDefaultService _dataService;

        public HomeController(ILogger<HomeController> logger, IDataDefaultService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        public async Task<IActionResult> Index()
        {
            await _dataService.GenerateDefaultDate();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

		[Route("Home/Error")]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if (statusCode == 400 || statusCode == 403 || statusCode == 404)
                {
                    var viewName = statusCode.ToString();
                    return View(viewName);
                }
                return View("400");
            }
            return View("400");
        }

		/// <summary>
		/// Status code 400
		/// </summary>
		/// <returns>Ошибка 400</returns>
		[Route("Home/400")]
		[HttpGet]
        public IActionResult GetException400()
        {
            try
            {
                throw new HttpRequestException("400");
            }
            catch
            {
                return View("400");
            }
        }

        /// <summary>
        /// Status code 403
        /// </summary>
        /// <returns>Ошибка 403</returns>
        [Route("Home/403")]
        [HttpGet]
        public IActionResult GetException403()
        {
            try
            {
                throw new HttpRequestException("403");
            }
            catch
            {
                return View("403");
            }
        }

        /// <summary>
        /// Status code 404
        /// </summary>
        /// <returns>Ошибка 404</returns>
        [Route("Home/404")]
        [HttpGet]
        public IActionResult GetException404()
        {
            try
            {
                throw new HttpRequestException("404");
            }
            catch
            {
                return View("404");
            }
        }
    }
}