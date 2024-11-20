using System.ComponentModel.DataAnnotations;

namespace SK2247A3.ViewModels
{
    public class MediaTypeBaseViewModel
    {
        [Key]
        public int MediaTypeId { get; set; }

        [Required]
        public string Name { get; set; }
    }

}