using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineArtGallery.Web.Models
{
    public class ExhibitionViewModel
    {
        public Guid Id { get; set; }
        public ExhibitionModel[] Exhibitions { get; set; }

        public MultiSelectList ArtistDropdown { get; set; }
        public MultiSelectList ArtworkDropdown { get; set; }


        #region NewExhibitionFields
        [Required]
        public string Title { get; set; }


        [Required(ErrorMessage = "The Artwork field is required.")]
        public List<Guid> SelectedArtworks { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        #endregion
    }

    public class ArtworkArtistName
    {
        public ArtworkArtistName(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
