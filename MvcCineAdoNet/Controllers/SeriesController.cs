using Microsoft.AspNetCore.Mvc;
using MvcCineAdoNet.Extensions;
using MvcCineAdoNet.Filters;
using MvcCineAdoNet.Models;
using MvcCineAdoNet.Repositories;
using System.Globalization;
using System.Security.Claims;

namespace MvcCineAdoNet.Controllers
{
    public class SeriesController : Controller
    {
        private IRepositoryCineBook repo;

        public SeriesController(IRepositoryCineBook repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> BuscadorSeries(int? idserie)
        {
            if(idserie != null)
            {
                Usuario user = HttpContext.Session.GetObject<Usuario>("usuario");
                await this.repo.InsertFavoritoSerieAsync(user.IdUsuario, idserie.Value);
            }
            List<Serie> series = await this.repo.GetSeriesAsync();
            return View(series);
        }

        [HttpPost]
        public async Task<IActionResult> BuscadorSeries(string titulo)
        {
            List<Serie> seriesEncontradas = await this.repo.BuscadorSeriesAsync(titulo);
            return View(seriesEncontradas);
        }

        public async Task<IActionResult> DetailsSerie(int idserie)
        {
            ViewSerieCompleta serie = await this.repo.FindSerieCompletaAsync(idserie);
            List<ActoresSerie> actoresSerie = await this.repo.GetActoresBySerieAsync(idserie);
            List<ComentarioSerie> comentarios = await this.repo.GetComentariosSerieAsync(idserie);
            ResumenDetailsSerie resumen = new ResumenDetailsSerie
            {
                SerieCompleta = serie,
                ActoresSerie = actoresSerie,
                ComentariosSerie = comentarios
            };
            return View(resumen);
        }

        [AuthorizeUsuarios]
        public async Task<IActionResult> CreateComentario(int idserie)
        {
            ViewData["idserie"] = idserie;
            return View();
        }

        [AuthorizeUsuarios]
        [HttpPost]
        public async Task<IActionResult> CreateComentario(string comentario, int idserie)
        {
            if(comentario == null)
            {
                ViewData["mensaje"] = "Debes introducir un comentario";
                return View();
            }
            int idusuario = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            DateTime fechaActual = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"), "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            await this.repo.InsertComentarioSerieAsync(idusuario, idserie, fechaActual, comentario);
            ViewData["mensaje"] = "Nuevo comentario introducido con éxito!!";
            return View();
            //mirara aqui como poner el return redirect to action pasando el id para que rediriga a los detalles
        }
    }
}
