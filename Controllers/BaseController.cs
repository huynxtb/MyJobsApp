using Microsoft.AspNetCore.Mvc;
using MyJobsApp.Models;

namespace MyJobsApp.Controllers
{
    public class BaseController : Controller
    {
        [NonAction]
        public IActionResult SuccessResponses(object result)
        {
            return Json(new ResponseViewModel()
            {
                Message = "Job run successfully!",
                Success = true,
                Result = result
            });
        }
        
        [NonAction]
        public IActionResult ErrorResponse(object result)
        {
            return Json(new ResponseViewModel()
            {
                Message = "Job run failure!",
                Success = false,
                Result = result
            });
        }
    }
}