using Microsoft.EntityFrameworkCore;
using OnlineArtGallery.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineArtGallery.Web.Data.Managers
{
    public class ArtistDataManager
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ArtworkDataManager _artworkDataManager;

        public ArtistDataManager(ApplicationDbContext dbContext, ArtworkDataManager artworkDataManager)
        {
            _dbContext = dbContext;
            _artworkDataManager = artworkDataManager;
        }

        public ArtistModel[] GetArtists()
        {
            var result = _dbContext
                .Artists
                .Include(x => x.Users)
                .ToArray();

            return result;
        }

        public ArtistModel[] GetArtists(List<Guid> artistsId)
        {
            var result = _dbContext
                .Artists
                .Include(x => x.Users)
                .Where(x => artistsId.Contains(x.Id))
                .ToArray();

            return result;
        }

        public ArtistModel GetOneArtist(Guid id)
        {
            var item = _dbContext.Artists.Include(x => x.Artworks).First(x => x.Id == id);

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
            var item = _dbContext
                .Artists
                .Include(x => x.Users)
                .Include(x => x.Artworks)
                .Include(x => x.Exhibitions)
                .First(x => x.Id == id);

            item.Users.Clear();
            _dbContext.SaveChanges();

            item.Exhibitions.Clear();
            _dbContext.SaveChanges();

            foreach(var artwork in item.Artworks)
            {
                _artworkDataManager.Delete(artwork.Id);
            }

            item.Artworks.Clear();
            _dbContext.SaveChanges();

            _dbContext.Artists.Remove(item);
            _dbContext.SaveChanges();
        }

        internal ArtistModel[] SearchArtist(string name, string surname, string placeOfBirth, string style)
        {
            var artists = _dbContext.Artists.Include(x => x.Artworks).AsQueryable();
            if (name != "" && name != null)
            {
                artists = artists.Where(x => x.Name.Contains(name));
            }

            if (surname != "" && surname != null)
            {
                artists = artists.Where(x => x.Surname.Contains(surname));
            }

            if (placeOfBirth != "" && placeOfBirth != null)
            {
                artists = artists.Where(x => x.PlaceOfBirth.Contains(placeOfBirth));
            }

            if (style != "" && style != null)
            {
                artists = artists.Where(x => x.Artworks.Any(t => t.Style.Style == style));
            }

            return artists.ToArray();
        }

    }
}

