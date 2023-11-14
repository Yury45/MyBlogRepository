using Microsoft.AspNetCore.Mvc;
using NLog;



namespace MyBlog.App.Controllers
{
    public class ErrorController : Controller
    {
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public ErrorController(){ }

		[Route("Error/Default")]
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
		[Route("Error/400")]
		[HttpGet]
        public IActionResult GetException400()
        {
            try
            {
				Log.Error($"User - {User.Identity.Name}: Сгенерирована ошибка 400.");

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
        [Route("Error/403")]
        [HttpGet]
        public IActionResult GetException403()
        {
            try
            {
				Log.Error($"User - {User.Identity.Name}: Сгенерирована ошибка 403.");

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
        [Route("Error/404")]
        [HttpGet]
        public IActionResult GetException404()
        {
            try
            {
				Log.Error($"User - {User.Identity.Name}: Сгенерирована ошибка 404.");

				throw new HttpRequestException("404");
            }
            catch
            {
                return View("404");
            }
        }

		/// <summary>
		/// Status code 500
		/// </summary>
		/// <returns>Ошибка 500</returns>
		[Route("Error/500")]
		[HttpGet]
		public IActionResult GetException500()
		{
			try
			{
				Log.Error($"User - {User.Identity.Name}: Сгенерирована ошибка 500.");

				throw new HttpRequestException("500");
			}
			catch
			{
				return View("500");
			}
		}

	}
}