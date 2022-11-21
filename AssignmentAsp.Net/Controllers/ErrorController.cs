using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentAsp.Net.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            /*
            switch(statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "404 N";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.QS = statusCodeResult.OriginalQueryString;
                    break;
            }
            */

            return View("~/Views/Error/NotFound.cshtml");
        }

        
        public IActionResult Index()
        {
            return View("~/Views/Error/ServerError.cshtml");
        }
    }
}
