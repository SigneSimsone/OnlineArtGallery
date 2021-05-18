using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OnlineArtGallery.Web.Models
{
    public class UserModel : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsBlocked { get; set; }

        public ICollection<ArtistModel> Artists { get; set; }
    }
}
