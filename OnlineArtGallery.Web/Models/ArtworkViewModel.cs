using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineArtGallery.Web.Models
{
    public class ArtworkViewModel
    {
        public Guid Id { get; set; }
     
        public ArtworkModel[] Artworks { get; set; }
        public string UserId { get; set; }

        public SelectList ArtistDropdown { get; set; }
        public SelectList StyleDropdown { get; set; }
        public SelectList AvailabilityDropdown { get; set; }
        public int SelectedAvailability { get; set; }


        #region NewArtworkFields
        [Required(ErrorMessage = "Title field error")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Artist field error")]
        public Guid SelectedArtist { get; set; }

        [Required(ErrorMessage = "Style field error")]
        public Guid SelectedStyle { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public float Price { get; set; }
        public bool Availability { get; set; }
        #endregion


        public IFormFile File { get; set; }
        public string FilePath { get; set; }
    }

}
