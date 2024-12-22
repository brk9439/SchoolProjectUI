using Microsoft.AspNetCore.Mvc;
using SchoolProjectUI.Models;
using System.Diagnostics;

namespace SchoolProjectUI.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			
			return View();
			//return RedirectToAction("Index", "Login");

		}

		public IActionResult Privacy()
		{
			return View();
		}
        public IActionResult ExamResults()
		{
			return View();
		}
        public IActionResult DetailExamResults()
		{
			return View();
		}
        public IActionResult StudentList()
		{
			return View();
		}
        public IActionResult ExamEntry()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
