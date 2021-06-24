using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineArtGallery.Web.Models
{
    public class SearchViewModel
    {
        public Guid Id { get; set; }

        public SelectList ArtistDropdown { get; set; }
        public SelectList StyleDropdown { get; set; }
        public SelectList AvailabilityDropdown { get; set; }
        

        public string Title { get; set; }
        public Guid SelectedArtist { get; set; }
        public Guid SelectedStyle { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public float Price { get; set; }
        public int SelectedAvailability { get; set; }
    }
}
