using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace OnlineArtGallery.Web.Models
{
    public class UserModel : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsBlocked { get; set; }

        public ICollection<ArtistModel> Artists { get; set; }
        public ICollection<ArtworkModel> Artworks { get; set; }
        public ICollection<FeedbackModel> Feedbacks { get; set; }
    }
}
