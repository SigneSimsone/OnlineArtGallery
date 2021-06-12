using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace OnlineArtGallery.Web.Models
{
    public class ArtworkViewModel
    {
        public Guid Id { get; set; }
     
        public ArtworkModel[] Artworks { get; set; }
        public string UserId { get; set; }

        public SelectList ArtistDropdown { get; set; }
        public SelectList StyleDropdown { get; set; }

        #region NewArtworkFields
        public string Title { get; set; }
        public Guid SelectedArtist { get; set; }
        public Guid SelectedStyle { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public float Price { get; set; }
        public bool Availability { get; set; }
        #endregion
    }
}
