using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels;
using MyBlog.Data.Models.Users;
using MyBlog.Data.Repositories.Interfaces;
using NLog;
using NLog.Fluent;
using System.Diagnostics;


namespace MyBlog.App.Controllers
{
    public class HomeController : Controller
    {
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();
		private readonly IDataDefaultService _dataService;

        public HomeController(IDataDefaultService dataService)
        {
            _dataService = dataService;
        }

        public async Task<IActionResult> Index()
        {
            await _dataService.GenerateDefaultDate();
			Log.Info($"User - {User.Identity.Name}: Сгенерированы стартовые данные.");

			return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
	}
}