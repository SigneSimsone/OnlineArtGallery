using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineArtGallery.Web.Models;

namespace OnlineArtGallery.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ArtistModel> Artists { get; set; }
        public DbSet<ArtworkModel> Artworks { get; set; }
        public DbSet<StyleModel> Styles { get; set; }
    }
}
