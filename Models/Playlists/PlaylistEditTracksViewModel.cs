using System.Collections.Generic;

namespace SK2247A3.ViewModels
{
    public class PlaylistEditTracksViewModel : PlaylistBaseViewModel
    {
        // Collection of track IDs to be added or removed
        public IEnumerable<int> SelectedTrackIds { get; set; }
    }

}