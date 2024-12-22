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
        public Task<BaseResponseModel<object>> Login(RequestLogin requestLogin);
        public Task<BaseResponseModel<object>> CreateSchool(RequestCreateSchool requestCreateSchool);
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

        public async Task<BaseResponseModel<object>> Login(RequestLogin requestLogin)
        {
            return await this.MethodPost(requestLogin, URL + "/api/Account/Login");
        }
        public async Task<BaseResponseModel<object>> CreateSchool(RequestCreateSchool requestCreateSchool)
        {
            return await this.MethodPost(requestCreateSchool, URL + "/api/School/CreateSchool");
        }
        #endregion
        #region Student
        #endregion

    }
}
