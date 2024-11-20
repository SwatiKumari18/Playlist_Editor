using SK2247A3.Data;
using SK2247A3.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace SK2247A3.ViewModels
{
    public class TrackWithDetailViewModel : TrackBaseViewModel
    {
        public string AlbumTitle { get; set; }

        public string AlbumArtistName { get; set; }

        public string MediaTypeName { get; set; }
    }
}