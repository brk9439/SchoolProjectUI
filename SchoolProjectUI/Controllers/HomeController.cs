using Microsoft.AspNetCore.Mvc;
using SchoolProjectUI.Models;
using System.Diagnostics;
using SchoolProject.Services.Service.Models;
using SchoolProject.Services.Service.Models.Response;
using SchoolProjectUI.Attribute;
using System.Net;
using SchoolProject.Services.Service;
using Newtonsoft.Json;

namespace SchoolProjectUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISchoolService _schoolService;

        public HomeController(ILogger<HomeController> logger, ISchoolService schoolService)
        {
            _logger = logger;
            _schoolService = schoolService;
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

        [HttpGet]
        public async Task<BaseResponseModel<object>> GetStudentList()
        {
            BaseResponseModel<object> response = new BaseResponseModel<object>();
            response.Success = true;
            try
            {
                var session = HttpContext.Session.GetSessionObject<List<ResponseLogin>>("User");

                var retVal = await _schoolService.GetStudentList(session.FirstOrDefault().SchoolId);
                if (retVal.Data is not null)
                {
                    response.Data = JsonConvert.DeserializeObject<List<ResponseStudentList>>(retVal.Data?.ToString());
                    response.StatusCode = retVal.StatusCode;
                    response.Message = retVal.Message;
                    response.Success = retVal.Success;
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NoContent;
                    response.Message = "Öðrenci bulunamadý";
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = ex.Message;
            }


            return response;
        }

    }
}
