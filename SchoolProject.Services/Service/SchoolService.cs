using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SchoolProject.Services.Extensions;
using SchoolProject.Services.Service.Models;
using SchoolProject.Services.Service.Models.Request;

namespace SchoolProject.Services.Service
{
    public interface ISchoolService
    {
        public Task<BaseResponseModel<object>> ManagerLogin(RequestManagerLogin requestManagerLogin);
        public Task<BaseResponseModel<object>> Login(RequestLogin requestLogin);
        public Task<BaseResponseModel<object>> CreateSchool(RequestCreateSchool requestCreateSchool);
        public Task<BaseResponseModel<object>> SchoolInfo(Guid id);
        public Task<BaseResponseModel<object>> StudentRegister(RequestStudentRegister requestStudentRegister);
        public Task<BaseResponseModel<object>> GetStudentList(Guid schoolGuid);
    }
    public class SchoolService : HttpClientService<BaseResponseModel<object>>, ISchoolService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string URL = "";
        public SchoolService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
#if DEBUG
            URL = _configuration.GetSection("ConnectionStrings:BaseUrl").Value;
#endif
#if RELEASE 
    URL = _configuration.GetSection("ConnectionStrings:BaseUrl").Value;
#endif
        }

        #region Login

        public async Task<BaseResponseModel<object>> ManagerLogin(RequestManagerLogin requestManagerLogin)
        {
            return await this.MethodPost(requestManagerLogin, URL + "/api/Account/ManagerLogin");
        }

        public async Task<BaseResponseModel<object>> Login(RequestLogin requestLogin)
        {
            return await this.MethodPost(requestLogin, URL + "/api/Account/Login");
        }
        public async Task<BaseResponseModel<object>> CreateSchool(RequestCreateSchool requestCreateSchool)
        {
            return await this.MethodPost(requestCreateSchool, URL + "/api/School/CreateSchool");
        }
        public async Task<BaseResponseModel<object>> StudentRegister(RequestStudentRegister requestStudentRegister)
        {
            return await this.MethodPost(requestStudentRegister, URL + "/api/User/StudentRegister");
        }

        public async Task<BaseResponseModel<object>> SchoolInfo(Guid id)
        {
            return await this.MethodGet(URL + "/api/School/SchoolInfo/" + id);
        }
        #endregion
        #region Student

        public async Task<BaseResponseModel<object>> GetStudentList(Guid schoolGuid)
        {
            return await this.MethodGet(URL + "/api/School/GetStudentList/" + schoolGuid);
        }
        #endregion

    }
}
