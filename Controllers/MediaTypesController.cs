using System.Web.Mvc;
using SK2247A3.Controllers;
using SK2247A3.Data;

namespace SK2247A3.Controllers
{
    public class MediaTypesController : Controller
    {
        // Reference to the Manager class
        private Manager m = new Manager();

        // GET: MediaTypes
        public ActionResult Index()
        {
            var mediatypes = m.MediaTypeGetAll();
            return View(mediatypes);
        }
    }
}
