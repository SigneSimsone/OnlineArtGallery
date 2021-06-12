using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineArtGallery.Web.Models
{
    public class StyleModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Style { get; set; }

        public List<ArtworkModel> Artworks { get; set; }

        public StyleModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
