using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Services.Service.Models
{
    public class BaseResponseModel<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public BaseResponseModel()
        {
            Success = true;
            StatusCode = HttpStatusCode.OK;

        }
    }
}
