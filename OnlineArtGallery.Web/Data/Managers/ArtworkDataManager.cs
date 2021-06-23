using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineArtGallery.Web.Data.Requests;
using OnlineArtGallery.Web.Models;
using System;
using System.Collections.Generic;
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
                .Include(x => x.Artist)
                .ToArray();

            return result;
        }

        public ArtworkModel[] GetArtworks(List<Guid> artworksId)
        {
            var result = _dbContext
                .Artworks
                .Include(x => x.Users)
                .Include(x => x.Artist)
                .Where(x => artworksId.Contains(x.Id))
                .ToArray();

            return result;
        }
        public ArtworkModel[] GetArtworksForExhibition(List<Guid> id)
        {
            List<ArtworkModel> ArtworksInExhibition = new List<ArtworkModel>();
            foreach (var i in id)
            {
                var result = _dbContext
                .Artworks
                .Include(y => y.Artist)
                .First(x => x.Id == i);

                ArtworksInExhibition.Add(result);
            }

            return ArtworksInExhibition.ToArray();
        }
        public ArtworkModel GetOneArtwork(Guid id)
        {
            var item = _dbContext.Artworks.Include(x => x.Users)
                                          .Include(x => x.Feedbacks)
                                          .Include(x => x.Artist)
                                          .Include(x => x.Style)
                                          .Include(x => x.Exhibitions)
                                          .First(x => x.Id == id);

            return item;
        }

        internal void AddArtwork(string title, ArtistModel artist, StyleModel style, int year, string description, string type, float price, bool availability, string filePath)
        {
            var item = new ArtworkModel()
            {
                Title = title,
                Artist = artist,
                Style = style,
                Year = year,
                Description = description,
                Type = type,
                Price = price,
                Availability = availability,
                FilePath = filePath
            };

            _dbContext.Artworks.Add(item);
            _dbContext.SaveChanges();
        }

        internal void Edit(Guid id, string title, ArtistModel artist, StyleModel style, int year, string description, string type, float price, bool availability, string filePath)
        {
            var item = _dbContext.Artworks.First(x => x.Id == id);
            item.Title = title;
            item.Artist = artist;
            item.Style = style;
            item.Year = year;
            item.Description = description;
            item.Type = type;
            item.Price = price;
            item.Availability = availability;
            item.FilePath = filePath;

            _dbContext.SaveChanges();
        }

        internal void FavoriteArtwork(Guid id, UserModel user)
        {
            var artwork = _dbContext.Artworks.Include(x => x.Users).First(x => x.Id == id);
            artwork.Users.Add(user);

            _dbContext.SaveChanges();
        }

        internal void UnFavoriteArtwork(Guid id, UserModel user)
        {
            var artwork = _dbContext.Artworks.Include(x => x.Users).First(x => x.Id == id);
            artwork.Users.Remove(user);

            _dbContext.SaveChanges();
        }

        internal ArtworkModel[] GetFavoriteArtworks(UserModel user)
        {
            var result = _dbContext.Artworks.Include(x => x.Users.Where(y => y == user)).ToArray();
            return result;
        }

        internal void Delete(Guid id)
        {
            var item = _dbContext
                .Artworks
                .Include(x => x.Users)
                .Include(x => x.Feedbacks)
                .Include(x => x.Orders)
                .Include(x => x.Exhibitions)
                .First(x => x.Id == id);

            item.Users.Clear();
            _dbContext.SaveChanges();

            item.Feedbacks.Clear();
            _dbContext.SaveChanges();

            item.Orders.Clear();
            _dbContext.SaveChanges();

            item.Exhibitions.Clear();
            _dbContext.SaveChanges();

            _dbContext.Artworks.Remove(item);
            _dbContext.SaveChanges();
        }


        internal void AddOrder(string address, ArtworkModel artwork, UserModel user)
        {
            var item = new OrderModel()
            {
                Address = address,
                Date = DateTime.Now,
                Artwork = artwork,
                User = user
            };

            _dbContext.Orders.Add(item);
            _dbContext.SaveChanges();
        }
        internal void EditAvailabilityAfterBuying(Guid id)
        {
            var item = _dbContext.Artworks.First(x => x.Id == id);
            item.Availability = false;

            _dbContext.SaveChanges();
        }
        public UserModel GetOneUser(string Id)
        {
            var item = _dbContext.Users.First(x => x.Id == Id);

            return item;
        }




        public FeedbackModel[] GetFeedbacks(Guid Id)
        {
            var result = _dbContext
                .Feedbacks
                .Where(x => x.Artwork.Id == Id)
                .ToArray();

            return result;
        }

        internal void AddFeedback(Guid Id, string comment, UserModel user)
        {
            var artwork = GetOneArtwork(Id);
            var item = new FeedbackModel()
            {
                Comment = comment,
                Date = DateTime.Now,
                User = user,
                Artwork = artwork
            };

            _dbContext.Feedbacks.Add(item);
            _dbContext.SaveChanges();
        }

        internal void DeleteFeedback(Guid Id)
        {
            var item = _dbContext.Feedbacks.First(x => x.Id == Id);
            _dbContext.Feedbacks.Remove(item);

            _dbContext.SaveChanges();
        }




        internal void AddStyle(string style)
        {
            var item = new StyleModel()
            {
                Style = style
            };

            _dbContext.Styles.Add(item);
            _dbContext.SaveChanges();
        }

        public StyleModel[] GetStyles()
        {
            var result = _dbContext
                .Styles
                .ToArray();

            return result;
        }

        public StyleModel GetOneStyle(Guid Id)
        {
            var item = _dbContext.Styles.First(x => x.Id == Id);

            return item;
        }


        internal ArtworkModel[] SearchArtwork(ArtworkSearchRequest model)
        {
            var artworks = _dbContext
                .Artworks
                .Include(x => x.Artist)
                .Include(x => x.Style)
                .AsQueryable();

            if (model.Title != "" && model.Title != null)
            {
                artworks = artworks.Where(x => x.Title.Contains(model.Title));
            }

            if (model.Artist != null && model.Artist.Name != "" && model.Artist != null)
            {
                artworks = artworks.Where(x => x.Artist.Name.Contains(model.Artist.Name));
            }

            if (model.Artist != null && model.Artist.Surname != "" && model.Artist.Surname != null)
            {
                artworks = artworks.Where(x => x.Artist.Surname.Contains(model.Artist.Surname));
            }

            if (model.Style != null && model.Style.Style != "" && model.Style.Style != null)
            {
                artworks = artworks.Where(x => x.Style.Style.Contains(model.Style.Style));
            }

            if (model.Year != 0)
            {
                artworks = artworks.Where(x => x.Year == model.Year);
            }

            if (model.Type != "" && model.Type != null)
            {
                artworks = artworks.Where(x => x.Type.Contains(model.Type));
            }

            if (model.Price != 0)
            {
                artworks = artworks.Where(x => x.Price <= model.Price);
            }

            if (model.Availability == 0)
            {
                artworks = artworks.Where(x => x.Availability == false);
            }

            if (model.Availability == 1)
            {
                artworks = artworks.Where(x => x.Availability == true);
            }

            return artworks.ToArray();
        }

    }
}

