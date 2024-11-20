using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SK2247A3.ViewModels
{
    public class PlaylistBaseViewModel
    {
        [Key]
        public int PlaylistId { get; set; }

        [Required]
        public string Name { get; set; }

        public int TracksCount { get; set; }
    }

}