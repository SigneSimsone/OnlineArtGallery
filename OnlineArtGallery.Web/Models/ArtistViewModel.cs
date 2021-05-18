using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineArtGallery.Web.Models
{
    public class ArtistViewModel
    {
        public ArtistModel[] Artists { get; set; }
        public string UserId { get; set; }
    }
}
