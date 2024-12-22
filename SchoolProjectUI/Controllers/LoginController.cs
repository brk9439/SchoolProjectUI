using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolProject.Services.Service;
using SchoolProject.Services.Service.Models;
using SchoolProject.Services.Service.Models.Request;
using System.Net;
using SchoolProject.Services.Service.Models.Response;

namespace SchoolProjectUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISchoolService _schoolService;

        public LoginController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<BaseResponseModel<object>> Login(RequestLogin requestLogin)
        {
            BaseResponseModel<object> response = new BaseResponseModel<object>();
            response.Success = true;
            try
            {
                var retVal = await _schoolService.Login(requestLogin);
                if (retVal.Data is not null)
                {
                    response.Data = JsonConvert.DeserializeObject<List<ResponseLogin>>(retVal.Data?.ToString());
                }

                response.StatusCode = retVal.StatusCode;
                response.Message = retVal.Message;
                response.Success = retVal.Success;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = ex.Message;
            }
            return response;
        }
        [HttpPost]
        public async Task<BaseResponseModel<object>> SchoolCreate(RequestCreateSchool requestCreateSchool)
        {
            BaseResponseModel<object> response = new BaseResponseModel<object>();
            response.Success = true;
            try
            {
                var retVal = await _schoolService.CreateSchool(requestCreateSchool);
                if (retVal.Data is not null)
                {
                    response.Data = JsonConvert.DeserializeObject<ResponseCreateSchool>(retVal.Data?.ToString());
                }

                response.StatusCode = retVal.StatusCode;
                response.Message = retVal.Message;
                response.Success = retVal.Success;
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
