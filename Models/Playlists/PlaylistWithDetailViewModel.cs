using SK2247A3.Data;
using SK2247A3.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SK2247A3.ViewModels
{
    public class PlaylistWithDetailViewModel : PlaylistBaseViewModel
    {
        public IEnumerable<TrackBaseViewModel> Tracks { get; set; }
    }
}