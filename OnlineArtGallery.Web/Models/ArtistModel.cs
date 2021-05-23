using System;
using System.Collections.Generic;

namespace OnlineArtGallery.Web.Models
{
    public class ArtistModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PlaceOfBirth { get; set; }

        public ArtistModel()
        {
            Id = Guid.NewGuid();
        }

        public ICollection<ArtworkModel> Artworks { get; set; }

        public ICollection<UserModel> Users { get; set; }
    }
}
