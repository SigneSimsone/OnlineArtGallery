using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineArtGallery.Web.Models
{
    public class StyleModel
    {
        [Key]
        public string NameofStyle { get; set; }

        public List<ArtworkModel> Artworks { get; set; }

    }
}
