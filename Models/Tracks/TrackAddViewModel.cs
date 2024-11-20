using System.ComponentModel.DataAnnotations;

namespace SK2247A3.ViewModels
{
    public class TrackAddViewModel : TrackBaseViewModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select an Album.")]
        public int SelectedAlbumId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a Media Type.")]
        public int SelectedMediaTypeId { get; set; }
    }
}
