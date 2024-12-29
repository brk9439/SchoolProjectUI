using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolProject.Services.Service;
using SchoolProject.Services.Service.Models;
using SchoolProject.Services.Service.Models.Request;
using System.Net;
using SchoolProject.Services.Service.Models.Response;
using SchoolProjectUI.Attribute;

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
        public IActionResult ManagerLogin()
        {
            return View();
        }
        public async Task<IActionResult> Register(Guid id)
        {
            var responseSchoolInfo = new List<ResponseSchoolInfo>();
            if (id != Guid.Empty)
            {
                var schoolInfo = await _schoolService.SchoolInfo(id);
                if (schoolInfo != null)
                {
                    responseSchoolInfo = JsonConvert.DeserializeObject<List<ResponseSchoolInfo>>(schoolInfo.Data?.ToString());
                    return View(responseSchoolInfo);
                }
                else
                {
                    return View(responseSchoolInfo);
                }


            }
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<BaseResponseModel<object>> ManagerLogin(RequestManagerLogin requestManagerLogin)
        {
            BaseResponseModel<object> response = new BaseResponseModel<object>();
            response.Success = true;
            try
            {
                var retVal = await _schoolService.ManagerLogin(requestManagerLogin);
                if (retVal.Data is not null)
                {
                    response.Data = JsonConvert.DeserializeObject<List<ResponseLogin>>(retVal.Data?.ToString());
                    HttpContext.Session.SetSessionObject("User", response.Data);
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
                    response.StatusCode = retVal.StatusCode;
                    response.Message = retVal.Message;
                    response.Success = retVal.Success;
                    HttpContext.Session.SetSessionObject("User", response.Data);
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NoContent;
                    response.Message = "Kullanıcı bilgileri hatalı!";
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

        [HttpPost]
        public async Task<BaseResponseModel<object>> StudentRegister(RequestStudentRegister requestStudentRegister)
        {
            BaseResponseModel<object> response = new BaseResponseModel<object>();
            response.Success = true;
            try
            {
                var retVal = await _schoolService.StudentRegister(requestStudentRegister);
                if (retVal.Data is not null || retVal.StatusCode == HttpStatusCode.OK)
                {
                    response.Data = null;
                    response.StatusCode = retVal.StatusCode;
                    response.Message = retVal.Message;
                    response.Success = retVal.Success;
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NoContent;
                    response.Message = "Kayıt işlemi için boş alanları doldurunuz!";
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
