using OnlineArtGallery.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var result = _dbContext.Artists.ToArray();

            return result;
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

        internal void Edit(Guid id)
        {
           
        }

        internal void Delete(Guid id)
        {
            var item = _dbContext.Artists.First(x => x.Id == id);
            _dbContext.Artists.Remove(item);

            _dbContext.SaveChanges();
        }
    }
}

