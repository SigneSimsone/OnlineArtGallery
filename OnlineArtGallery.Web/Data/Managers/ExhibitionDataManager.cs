using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineArtGallery.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineArtGallery.Web.Data.Managers
{
    public class ExhibitionDataManager : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public ExhibitionDataManager(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ExhibitionModel[] GetExhibitions()
        {
            var result = _dbContext
                .Exhibitions
                .Include(x => x.Artworks)
                .Include(x => x.Artists)
                .ToArray();

            return result;
        }

        public ExhibitionModel[] GetExhibitions(List<Guid> exhibitionsId)
        {
            var result = _dbContext
                .Exhibitions
                .Include(x => x.Artworks)
                .Include(x => x.Artists)
                .Where(x => exhibitionsId.Contains(x.Id))
                .ToArray();

            return result;
        }

        public ExhibitionModel GetOneExhibition(Guid Id)
        {
            var item = _dbContext
                .Exhibitions
                .Include(x => x.Artworks)
                .Include(x => x.Artists)
                .First(x => x.Id == Id);

            return item;
        }

        internal void AddExhibition(string title, ArtistModel[] artists, ArtworkModel[] artworks, DateTime startDate, DateTime endDate)
        {
            var item = new ExhibitionModel()
            {
                Title = title,
                Artists = artists,
                Artworks = artworks,
                StartDate = startDate,
                EndDate = endDate
            };

            _dbContext.Exhibitions.Add(item);
            _dbContext.SaveChanges();
        }

        internal void Edit(Guid id, string title, ArtistModel[] artists, ArtworkModel[] artworks, DateTime startDate, DateTime endDate)
        {
            var item = _dbContext.Exhibitions.Include(x => x.Artworks).Include(x => x.Artists).First(x => x.Id == id);
            item.Title = title;
            item.Artists = artists;
            item.Artworks = artworks;
            item.StartDate = startDate;
            item.EndDate = endDate;

            _dbContext.SaveChanges();
        }

        internal void Delete(Guid id)
        {
            var item = _dbContext.Exhibitions.First(x => x.Id == id);
            _dbContext.Exhibitions.Remove(item);

            _dbContext.SaveChanges();
        }

        internal ExhibitionModel[] SearchExhibition(string title, string name, string surname, string artworkTitle, DateTime startDate, DateTime endDate)
        {
            var exhibitions = _dbContext
                .Exhibitions
                .Include(x => x.Artworks)
                .Include(x => x.Artists)
                .AsQueryable();

            if (title != "" && title != null)
            {
                exhibitions = exhibitions.Where(x => x.Title.Contains(title));
            }

            if (name != "" && name != null)
            {
                exhibitions = exhibitions.Where(x => x.Artists.Any(t => t.Name == name));
            }

            if (surname != "" && surname != null)
            {
                exhibitions = exhibitions.Where(x => x.Artists.Any(t => t.Surname == surname));
            }

            if (artworkTitle != "" && artworkTitle != null)
            {
                exhibitions = exhibitions.Where(x => x.Artworks.Any(t => t.Title == artworkTitle));
            }

            if (startDate > DateTime.MinValue)
            {
                exhibitions = exhibitions.Where(x => x.StartDate.Date == startDate.Date);
            }

            if (endDate > DateTime.MinValue)
            {
                exhibitions = exhibitions.Where(x => x.EndDate.Date == endDate.Date);
            }

            return exhibitions.ToArray();
        }
    }
}
