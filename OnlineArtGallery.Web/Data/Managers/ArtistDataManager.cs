using Microsoft.EntityFrameworkCore;
using OnlineArtGallery.Web.Models;
using System;
using System.Linq;

namespace OnlineArtGallery.Web.Data.Managers
{
    public class ArtistDataManager
    {
        private readonly ApplicationDbContext _dbContext;

        public ArtistDataManager(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ArtistModel[] GetArtists()
        {
            var result = _dbContext
                .Artists
                .Include(x => x.Users)
                .ToArray();

            return result;
        }
        public ArtistModel GetOneArtist(Guid id)
        {
            var item = _dbContext.Artists.First(x => x.Id == id);

            return item;
        }

        internal void AddArtist(string name, string surname, string place)
        {
            var item = new ArtistModel()
            {
                Name = name,
                Surname = surname,
                PlaceOfBirth = place
            };

            _dbContext.Artists.Add(item);
            _dbContext.SaveChanges();
        }

        internal void Edit(Guid id, string name, string surname, string place)
        {
            var item = _dbContext.Artists.First(x => x.Id == id);
            item.Name = name;
            item.Surname = surname;
            item.PlaceOfBirth = place;

            _dbContext.SaveChanges();
        }

        internal void FavoriteArtist(Guid id, UserModel user)
        {
            var artist = _dbContext.Artists.Include(x => x.Users).First(x => x.Id == id);
            artist.Users.Add(user);

            _dbContext.SaveChanges();
        }

        internal void UnFavoriteArtist(Guid id, UserModel user)
        {
            var artist = _dbContext.Artists.Include(x => x.Users).First(x => x.Id == id);
            artist.Users.Remove(user);

            _dbContext.SaveChanges();
        }

        internal ArtistModel[] GetFavoriteArtists(UserModel user)
        {
            var result = _dbContext.Artists.Include(x => x.Users.Where(y => y == user)).ToArray();
            return result;
        }

        internal void Delete(Guid id)
        {
            var item = _dbContext.Artists.First(x => x.Id == id);
            _dbContext.Artists.Remove(item);

            _dbContext.SaveChanges();
        }
    }
}

