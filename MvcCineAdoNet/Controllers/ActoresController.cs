using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using MvcCineAdoNet.Models;
using MvcCineAdoNet.Repositories;

namespace MvcCineAdoNet.Controllers
{
    public class ActoresController : Controller
    {
        private IRepositoryCine repo;

        public ActoresController(IRepositoryCine repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Actor> actores = this.repo.GetActores();
            return View(actores);
        }
    }
}
