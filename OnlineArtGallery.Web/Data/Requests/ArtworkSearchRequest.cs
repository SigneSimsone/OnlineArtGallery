using OnlineArtGallery.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineArtGallery.Web.Data.Requests
{
    public class ArtworkSearchRequest
    {
        public string Title { get; set; }
        public ArtistModel Artist { get; set; }
        public StyleModel Style { get; set; }
        public int Year { get; set; }
        public string Type { get; set; }
        public float Price { get; set; }
        public int Availability { get; set; }
    }
}
