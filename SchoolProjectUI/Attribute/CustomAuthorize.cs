
using SchoolProject.Services.Service.Models.Response;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SchoolProjectUI.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CustomAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var context = filterContext.HttpContext;
            ResponseLogin session = new ResponseLogin();
            session = context.Session.GetSessionObject<ResponseLogin>("User");
            if (session != null)
            {
                var isAjax = context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
                if (isAjax)
                {
                    JsonResult result = ((Controller)filterContext.Controller).Json("SessionTimeout");
                    filterContext.Result = result;
                }
                else
                {
                    context.Response.Redirect("/Login/Login");
                }

            }
            else
            {
                context.Response.Redirect("/Login/Login");
            }


            base.OnActionExecuting(filterContext);
        }
    }
}
