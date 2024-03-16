using Microsoft.AspNetCore.Mvc;
using MvcCineAdoNet.Filters;

namespace MvcCineAdoNet.Controllers
{
    public class AdminController : Controller
    {
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
