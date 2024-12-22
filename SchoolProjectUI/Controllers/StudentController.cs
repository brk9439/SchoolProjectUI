using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolProject.Services.Service;
using SchoolProject.Services.Service.Models;

namespace SchoolProjectUI.Controllers
{
    public class StudentController : Controller
    {
        private readonly ISchoolService _schoolService;

        public StudentController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult FailedExamResults()
        {
            return View();
        }
        
    }
}
