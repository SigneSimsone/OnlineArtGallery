using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineArtGallery.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineArtGallery.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ArtistModel> Artists { get; set; }
    }
}
