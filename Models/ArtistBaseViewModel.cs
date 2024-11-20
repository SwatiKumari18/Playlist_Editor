using System.ComponentModel.DataAnnotations;

namespace SK2247A3.ViewModels
{
    public class ArtistBaseViewModel
    {
        [Key]
        public int ArtistId { get; set; }

        [Required]
        public string Name { get; set; }
    }

}