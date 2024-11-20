using System.Collections.Generic;
using System.Web.Mvc;

namespace SK2247A3.ViewModels
{

    public class PlaylistEditTracksFormViewModel : PlaylistBaseViewModel
    {
        // List for all available tracks
        public MultiSelectList AllTracks { get; set; }

        // IDs of the selected tracks that are currently in the playlist
        public List<int> SelectedTrackIds { get; set; }

        // Optional: List of track details for tracks currently in the playlist
        public IEnumerable<TrackBaseViewModel> CurrentTracks { get; set; }

        
    }

}