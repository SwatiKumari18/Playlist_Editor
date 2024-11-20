using System.Web.Mvc;
using SK2247A3.Controllers;
using SK2247A3.Data;

namespace SK2247A3.Controllers
{
    public class ArtistsController : Controller
    {
        // Reference to the Manager class
        private Manager m = new Manager();

        // GET: Artists
        public ActionResult Index()
        {
            var artists = m.ArtistGetAll();
            return View(artists);
        }
    }
}
