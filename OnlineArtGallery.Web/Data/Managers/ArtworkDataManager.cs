using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineArtGallery.Web.Models;
using System;
using System.Linq;

namespace OnlineArtGallery.Web.Data.Managers
{
    public class ArtworkDataManager : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ArtworkDataManager(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ArtworkModel[] GetArtworks()
        {
            var result = _dbContext
                .Artworks
                .Include(x => x.Users)
                .ToArray();

            return result;
        }
        public ArtworkModel GetOneArtwork(Guid id)
        {
            var item = _dbContext.Artworks.First(x => x.Id == id);

            return item;
        }

        internal void AddArtwork(string title, int year, string description, string type, float price)
        {
            var item = new ArtworkModel()
            {
                Title = title,
                Year = year,
                Description = description,
                Type = type,
                Price = price,
                Availability = true
            };

            _dbContext.Artworks.Add(item);
            _dbContext.SaveChanges();
        }

        internal void Edit(Guid id, string title, int year, string description, string type, float price, bool availability)
        {
            var item = _dbContext.Artworks.First(x => x.Id == id);
            item.Title = title;
            item.Year = year;
            item.Description = description;
            item.Type = type;
            item.Price = price;
            item.Availability = availability;

            _dbContext.SaveChanges();
        }

        internal void FavoriteArtwork(Guid id, UserModel user)
        {
            var artwork = _dbContext.Artists.Include(x => x.Users).First(x => x.Id == id);
            artwork.Users.Add(user);

            _dbContext.SaveChanges();
        }

        internal void UnFavoriteArtwork(Guid id, UserModel user)
        {
            var artwork = _dbContext.Artists.Include(x => x.Users).First(x => x.Id == id);
            artwork.Users.Remove(user);

            _dbContext.SaveChanges();
        }

        internal void Favorite()
        {

        }

        internal void Delete(Guid id)
        {
            var item = _dbContext.Artworks.First(x => x.Id == id);
            _dbContext.Artworks.Remove(item);

            _dbContext.SaveChanges();
        }
    }
}

