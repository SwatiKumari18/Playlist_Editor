using AutoMapper;
using SK2247A3.Data;
using SK2247A3.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

// ************************************************************************************
// WEB524 Project Template V1 == 2237-f72b4c32-cf3e-4d6e-a6e0-d49d6119c5ab
//
// By submitting this assignment you agree to the following statement.
// I declare that this assignment is my own work in accordance with the Seneca Academic
// Policy. No part of this assignment has been copied manually or electronically from
// any other source (including web sites) or distributed to other students.
// ************************************************************************************

namespace SK2247A3.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper instance
        public IMapper mapper;

        public Manager()
        {
            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Albums Mappping
                cfg.CreateMap<Album, AlbumBaseViewModel>();

                // Artists Mappping
                cfg.CreateMap<Artist, ArtistBaseViewModel>();

                // MediaTypes Mappping
                cfg.CreateMap<MediaType, MediaTypeBaseViewModel>();

                // Tracks Mapping
                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<Track, TrackWithDetailViewModel>()
                    .ForMember(dest => dest.AlbumTitle, opt => opt.MapFrom(src => src.Album.Title))
                    .ForMember(dest => dest.AlbumArtistName, opt => opt.MapFrom(src => src.Album.Artist.Name))
                    .ForMember(dest => dest.MediaTypeName, opt => opt.MapFrom(src => src.MediaType.Name));

                //Playlists Mapping
                cfg.CreateMap<Playlist, PlaylistBaseViewModel>()
                    .ForMember(dest => dest.TracksCount, opt => opt.MapFrom(src => src.Tracks.Count));
                cfg.CreateMap<Playlist, PlaylistWithDetailViewModel>();
                cfg.CreateMap<Playlist, PlaylistEditTracksFormViewModel>()
                    .ForMember(dest => dest.TracksCount, opt => opt.MapFrom(src => src.Tracks.Count))
                    .ForMember(dest => dest.SelectedTrackIds, opt => opt.MapFrom(src => src.Tracks.Select(t => t.TrackId)))
                    .ForMember(dest => dest.CurrentTracks, opt => opt.MapFrom(src => src.Tracks));
                cfg.CreateMap<PlaylistEditTracksViewModel, Playlist>()
                    .ForMember(dest => dest.Tracks, opt => opt.Ignore());
                cfg.CreateMap<PlaylistBaseViewModel, PlaylistEditTracksFormViewModel>();
                cfg.CreateMap<PlaylistBaseViewModel, PlaylistEditTracksViewModel>();

                // Mapping from Track entity to TrackBaseViewModel (if Track details are needed in the playlist views)
                //CreateMap<Track, TrackBaseViewModel>()
                //    .ForMember(dest => dest.NameFull, opt => opt.MapFrom(src => src.NameFull))
                //    .ForMember(dest => dest.NameShort, opt => opt.MapFrom(src => src.NameShort));

            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // Get all Albums
        public IEnumerable<AlbumBaseViewModel> AlbumGetAll()
        {
            var albums = (from a in ds.Albums
                          orderby a.Title
                          select a).ToList();
            return mapper.Map<IEnumerable<AlbumBaseViewModel>> (albums);
        }

        // Get all Artists
        public IEnumerable<ArtistBaseViewModel> ArtistGetAll()
        {
            var artists = (from a in ds.Artists
                           orderby a.Name
                           select a).ToList();
            return mapper.Map<IEnumerable<ArtistBaseViewModel>>(artists);
        }

        // Get all MediaTypes
        public IEnumerable<MediaTypeBaseViewModel> MediaTypeGetAll()
        {
            var mediatypes = (from m in ds.MediaTypes
                              orderby m.Name
                              select m).ToList();
            return mapper.Map<IEnumerable<MediaTypeBaseViewModel>>(mediatypes);
        }

        // Get all Tracks
        public IEnumerable<TrackWithDetailViewModel> TrackGetAll()
        {
            var tracks = (from t in ds.Tracks.Include("Album").Include("Album.Artist").Include("MediaType")
                          orderby t.Name
                          select t).ToList();
            return mapper.Map<IEnumerable<TrackWithDetailViewModel>>(tracks);
        }

        //Get Track By Id
        public TrackWithDetailViewModel TrackGetById(int id)
        {
            var track = ds.Tracks.Include("Album").Include("Album.Artist").Include("MediaType")
                            .SingleOrDefault(i => i.TrackId == id);

            return track == null ? null : mapper.Map<TrackWithDetailViewModel>(track);
        }

        // Add a new track
        public TrackWithDetailViewModel TrackAdd(TrackAddViewModel newTrack)
        {
            // Ensuring that the incoming data is valid
            if (newTrack == null) return null;

            var album = ds.Albums.Find(newTrack.SelectedAlbumId);
            var mediaType = ds.MediaTypes.Find(newTrack.SelectedMediaTypeId);

            if (album == null || mediaType == null) return null;

            var addedTrack = new Track
            {
                Name = newTrack.Name,
                Composer = newTrack.Composer,
                Milliseconds = newTrack.Milliseconds,
                UnitPrice = newTrack.UnitPrice,
                Album = album,
                MediaType = mediaType
            };

            ds.Tracks.Add(addedTrack);
            ds.SaveChanges();

            return mapper.Map<TrackWithDetailViewModel>(addedTrack);
        }

        //Get all Playlists
        public IEnumerable<PlaylistBaseViewModel> PlaylistGetAll()
        {
            var playlists = (from p in ds.Playlists.Include("Tracks")
                             orderby p.Name
                             select new PlaylistBaseViewModel
                             {
                                 PlaylistId = p.PlaylistId,
                                 Name = p.Name,
                                 TracksCount = p.Tracks.Count()
                             }).ToList();
            return mapper.Map<IEnumerable<PlaylistBaseViewModel>>(playlists);
        }

        // Get Playlist by Id
        public PlaylistWithDetailViewModel PlaylistGetById(int id)
        {
            var playlist = ds.Playlists.Include("Tracks")
                                .SingleOrDefault(p => p.PlaylistId == id);

            return playlist == null ? null : mapper.Map<PlaylistWithDetailViewModel>(playlist);
        }

        //Edit Playlist
        public PlaylistBaseViewModel PlaylistEditTracks(PlaylistEditTracksViewModel viewModel)
        {
            // Find the existing playlist including its tracks
            var playlist = ds.Playlists.Include("Tracks").SingleOrDefault(p => p.PlaylistId == viewModel.PlaylistId);
            if (playlist == null) return null;

            // Update the playlist's tracks
            // Clear current tracks and add only the selected tracks
            playlist.Tracks.Clear();
            var selectedTracks = ds.Tracks.Where(t => viewModel.SelectedTrackIds.Contains(t.TrackId)).ToList();
            playlist.Tracks = selectedTracks;

            ds.SaveChanges();
            //return true;
            //if (viewModel.SelectedTrackIds != null)
            //{
            //    // Find the selected tracks based on their IDs
            //    foreach (var trackId in viewModel.SelectedTrackIds)
            //    {
            //        var track = ds.Tracks.Find(trackId);
            //        if (track != null) playlist.Tracks.Add(track);
            //    }
            //}

            //// Save changes to the data store
            //ds.SaveChanges();

            // Return the updated playlist view model
            return new PlaylistBaseViewModel
            {
                PlaylistId = playlist.PlaylistId,
                Name = playlist.Name,
                TracksCount = playlist.Tracks.Count
            };
        }



        ////Get Playlist By Id
        //public PlaylistBaseViewModel PlaylistGetById(int id)
        //{
        //    // Find the playlist by ID and include the related Tracks collection
        //    var playlist = ds.Playlists
        //                     .Include("Tracks")
        //                     .SingleOrDefault(p => p.PlaylistId == id);

        //    // Return null if the playlist is not found
        //    if (playlist == null) return null;

        //    // Map the playlist entity to PlaylistBaseViewModel
        //    return new PlaylistBaseViewModel
        //    {
        //        PlaylistId = playlist.PlaylistId,
        //        Name = playlist.Name,
        //        Tracks = playlist.Tracks.Select(t => new TrackBaseViewModel
        //        {
        //            TrackId = t.TrackId,
        //            Name = t.Name,
        //            Composer = t.Composer,
        //            Milliseconds = t.Milliseconds,
        //            UnitPrice = t.UnitPrice
        //        }).ToList()
        //    };
        //}


        ////Update Playlist
        //public bool UpdatePlaylistTracks(int playlistId, IEnumerable<int> selectedTrackIds)
        //{
        //    var playlist = ds.Playlists.Include("Tracks").SingleOrDefault(p => p.PlaylistId == playlistId);

        //    if (playlist == null) return false;

        //    playlist.Tracks.Clear();

        //    foreach (var id in selectedTrackIds)
        //    {
        //        var track = ds.Tracks.Find(id);
        //        if (track != null) playlist.Tracks.Add(track);
        //    }

        //    ds.SaveChanges();
        //    return true;
        //}


    }
}