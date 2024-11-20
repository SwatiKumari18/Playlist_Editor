using AutoMapper;
using SK2247A3.Data;
using SK2247A3.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SK2247A3.Controllers
{
    public class PlaylistsController : Controller
    {
        // Reference to the Manager class
        private Manager m = new Manager();

        // GET: Playlists
        public ActionResult Index()
        {
            var playlists = m.PlaylistGetAll();
            //foreach (var item in playlists)
            //{
            //    Console.WriteLine($"Playlist: {item.Name}, TracksCount: {item.TracksCount}");
            //}
            return View(playlists);
        }

        // Get: Playlist/{id}
        public ActionResult Details(int id)
        {
            var playlist = m.PlaylistGetById(id);
            if (playlist == null)
            {
                return HttpNotFound();
            }

            return View(playlist);
        }

        // GET: Playlist/Edit/{id}
        public ActionResult Edit(int id)
        {
            var playlist = m.PlaylistGetById(id);
            if (playlist == null) return HttpNotFound();

            var formModel = new PlaylistEditTracksFormViewModel
            {
                PlaylistId = playlist.PlaylistId,
                Name = playlist.Name,
                TracksCount = playlist.TracksCount,
                //SelectedTrackIds = CurrentTracks.Select(t => t.TrackId),
                AllTracks = new SelectList(m.TrackGetAll(), "TrackId", "NameFull"),
                CurrentTracks = playlist.Tracks.Select(t => new TrackBaseViewModel
                {
                    TrackId = t.TrackId,
                    Name = t.NameShort

                })
            };
            formModel.SelectedTrackIds = formModel.CurrentTracks.Select(t => t.TrackId).ToList();

            return View(formModel);
        }

        // POST: Playlist/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlaylistEditTracksFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //var playlist = m.PlaylistEditTracks(viewModel);
                return RedirectToAction("Details", "Playlists", new { id = viewModel.PlaylistId });
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage); // or log this to see what went wrong
            }
            // Return the view with the model to display errors
            return View(viewModel);
            //return RedirectToAction("Index", "Playlists");
        }
            

            //if (ModelState.IsValid)
            //{
            //    // Try to update the playlist using the manager
            //    var success = m.PlaylistEditTracks(viewModel);

            //    if (success)
            //    {
            //        // If successful, redirect to a details or confirmation page
            //        return RedirectToAction("Details", new { id = viewModel.PlaylistId });
            //    }
            //}

            // If we need to re-render the form, create the PlaylistEditTracksFormViewModel 
            // with all necessary data for the view
            //var playlist = m.PlaylistGetById(viewModel.PlaylistId);
            //if (playlist == null)
            //{
            //    return HttpNotFound();
            //}

            
            //var formModel = new PlaylistEditTracksFormViewModel
            //{
            //    PlaylistId = playlist.PlaylistId,
            //    Name = playlist.Name,
            //    TracksCount = playlist.TracksCount,
            //    //SelectedTrackIds = CurrentTracks.Select(t => t.TrackId),
            //    AllTracks = new SelectList(m.TrackGetAll(), "TrackId", "NameFull"),
            //    CurrentTracks = playlist.Tracks.Select(t => new TrackBaseViewModel
            //    {
            //        TrackId = t.TrackId,
            //        Name = t.NameShort

            //    })
            //};
            //formModel.SelectedTrackIds = formModel.CurrentTracks.Select(t => t.TrackId).ToList();

            // Render the form view with the correct model type
            //return RedirectToAction("Index", "Playlists");
        //}



        //public ActionResult Index(int id)
        //{
        //    // Retrieve the playlist details by ID
        //    var playlist = m.PlaylistGetById(id);
        //    if (playlist == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    // Create the Form ViewModel
        //    var form = new PlaylistEditTracksFormViewModel
        //    {
        //        PlaylistId = playlist.PlaylistId,
        //        PlaylistName = playlist.Name,
        //        AllTracks = new MultiSelectList(m.TrackGetAll(), "TrackId", "Name"),
        //        SelectedTrackIds = playlist.Tracks.Select(t => t.TrackId).ToList(),
        //        SelectedTracks = playlist.Tracks
        //        //AvailableTracks = new MultiSelectList(m.TrackGetAll(), "TrackId", "Name"),
        //        //CurrentTracks = playlist.Tracks.Select(t => t.TrackId).ToList()
        //        //CurrentTracks = playlist.Tracks
        //    };

        //    return View("Index", form);
        //}

        //public ActionResult EditTracks(int id)
        //{
        //    var playlist = m.PlaylistGetById(id);

        //    if (playlist == null) return HttpNotFound();

        //    var formModel = new PlaylistEditTracksFormViewModel
        //    {
        //        PlaylistId = playlist.PlaylistId,
        //        PlaylistName = playlist.Name,
        //        AllTracks = new MultiSelectList(m.TrackGetAll(), "TrackId", "Name"),
        //        SelectedTrackIds = playlist.Tracks.Select(t => t.TrackId).ToList(),
        //        SelectedTracks = playlist.Tracks
        //        //AvailableTracks = new MultiSelectList(m.TrackGetAll(), "TrackId", "Name"),
        //        //CurrentTracks = playlist.Tracks
        //    };

        //    return View(formModel);
        //}

        //[HttpPost]
        //public ActionResult Index(PlaylistEditTracksViewModel form)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //var result = m.PlaylistEditTracks(form);
        //        var result = m.UpdatePlaylistTracks(form.PlaylistId, form.SelectedTrackIds);
        //        if (result)
        //        {
        //            return RedirectToAction("Details", new { id = form.PlaylistId });
        //        }
        //    }
        //    // Re-display form with errors if submission fails
        //    return View(form);
        //}


        //[HttpPost]
        //public ActionResult EditTracks(PlaylistEditTracksViewModel model)
        //{
        //    if (!ModelState.IsValid) return View(model);

        //    var success = m.UpdatePlaylistTracks(model.PlaylistId, model.SelectedTrackIds);

        //    if (success) return RedirectToAction("Details", new { id = model.PlaylistId });

        //    return View(model);
        //}


    }
}