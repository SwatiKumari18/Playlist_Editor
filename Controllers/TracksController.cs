using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using SK2247A3.Controllers;
using SK2247A3.Data;
using SK2247A3.ViewModels;

namespace SK2247A3.Controllers
{
    public class TracksController : Controller
    {
        // Reference to the Manager class
        private Manager m = new Manager();

        private readonly Manager _manager;

        public TracksController()
        {
            _manager = new Manager();
        }

        // GET: Tracks
        public ActionResult Index()
        {
            var tracks = m.TrackGetAll();
            return View(tracks);
        }

        // GET: Tracks/Details{id}
        public ActionResult Details(int id)
        {
            var track = m.TrackGetById(id);
            if (track == null)
            {
                return HttpNotFound();
            }
            return View(track);
        }

        // GET: Tracks/Add
        public ActionResult Add()
        {
            var viewModel = new TrackAddFormViewModel
            {
                Albums = m.AlbumGetAll().Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = a.AlbumId.ToString(),
                    Text = a.Title
                }),

                MediaTypes = m.MediaTypeGetAll().Select(m => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = m.MediaTypeId.ToString(),
                    Text = m.Name
                }),

                SelectedAlbumId = 156,
                SelectedMediaTypeId = 1
            };

            return View(viewModel);
        }

        // POST: Tracks/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(TrackAddFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var track = new TrackAddViewModel
                {
                    Name = viewModel.Name,
                    Composer = viewModel.Composer,
                    Milliseconds = viewModel.Milliseconds,
                    UnitPrice = viewModel.UnitPrice,
                    SelectedAlbumId = viewModel.SelectedAlbumId,
                    SelectedMediaTypeId = viewModel.SelectedMediaTypeId
                };

                var addedTrack = m.TrackAdd(track);

                return RedirectToAction("Details", new { id = addedTrack.TrackId });
            }

            // Redisplay form
            //viewModel.Albums = m.AlbumGetAll().Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            //{
            //    Value = a.AlbumId.ToString(),
            //    Text = a.Title
            //});
            //viewModel.MediaTypes = m.MediaTypeGetAll().Select(m => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            //{
            //    Value = m.MediaTypeId.ToString(),
            //    Text = m.Name
            //});
            //viewModel.SelectedAlbumId = 156;
            //viewModel.SelectedMediaTypeId = 1;


            return View(viewModel);

        }
}
}
