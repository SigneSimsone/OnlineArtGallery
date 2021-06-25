using System.Collections.Generic;

namespace OnlineArtGallery.Web.Models
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsBlocked { get; set; }

        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public ICollection<ArtistModel> Artists { get; set; }
        public ICollection<ArtworkModel> Artworks { get; set; }
        public ICollection<FeedbackModel> Feedbacks { get; set; }

        public IList<string> Roles { get; set; }
        public string RolesAsString
        {
            get
            {
                return string.Join(", ", Roles);             
            }
        }

    }
}
