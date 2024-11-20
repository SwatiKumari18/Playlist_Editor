using System.ComponentModel.DataAnnotations;

namespace SK2247A3.ViewModels
{
    public class AlbumBaseViewModel
    {
        [Key]
        public int AlbumId { get; set; }

        [Required]
        public string Title { get; set; }
    }

}