using SK2247A3.Data;
using SK2247A3.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace SK2247A3.ViewModels
{
    public class TrackAddFormViewModel : TrackBaseViewModel
    {
        public IEnumerable<SelectListItem> Albums { get; set; }
        public IEnumerable<SelectListItem> MediaTypes { get; set; }
        public int SelectedAlbumId { get; set; }
        public int SelectedMediaTypeId { get; set; }
    }
}