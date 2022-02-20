using Microsoft.AspNetCore.Mvc;

namespace MyJobsApp.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return Json("App is running...");
        }
    }
}