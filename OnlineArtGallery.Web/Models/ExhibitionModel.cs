using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineArtGallery.Web.Models
{
    public class ExhibitionModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ExhibitionModel()
        {
            Id = Guid.NewGuid();
        }
        public ICollection<ArtistModel> Artists { get; set; }
        public ICollection<ArtworkModel> Artworks { get; set; }
    }
}
