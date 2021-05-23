using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineArtGallery.Web.Models
{
    public class ArtworkViewModel
    {
        public ArtworkModel[] Artworks { get; set; }
        public string UserId { get; set; }
    }
}
