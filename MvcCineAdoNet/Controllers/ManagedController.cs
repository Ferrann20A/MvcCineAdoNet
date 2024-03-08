using Microsoft.AspNetCore.Mvc;

namespace MvcCineAdoNet.Controllers
{
    public class ManagedController : Controller
    {
        public IActionResult AccesoDenegado()
        {
            return View();
        }
    }
}
