using System.Web.Mvc;
using SK2247A3.Controllers;
using SK2247A3.Data;

namespace SK2247A3.Controllers
{
    public class AlbumsController : Controller
    {
        // Reference to the Manager class
        private Manager m = new Manager();

        // GET: Albums
        public ActionResult Index()
        {
            var albums = m.AlbumGetAll();
            return View(albums);
        }
    }
}
