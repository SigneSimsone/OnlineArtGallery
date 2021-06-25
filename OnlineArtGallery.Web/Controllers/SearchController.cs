using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineArtGallery.Web.Data.Managers;
using OnlineArtGallery.Web.Data.Requests;
using OnlineArtGallery.Web.Models;
using System;
using System.Linq;

namespace OnlineArtGallery.Web.Controllers
{

    public class SearchController : Controller
    {
        private readonly ArtistDataManager _artistDataManager;
        private readonly ArtworkDataManager _artworkDataManager;
        private readonly ExhibitionDataManager _exhibitionDataManager;

        public SearchController(ArtistDataManager artistDataManager, ArtworkDataManager artworkDataManager, ExhibitionDataManager exhibitionDataManager)
        {
            _artistDataManager = artistDataManager;
            _artworkDataManager = artworkDataManager;
            _exhibitionDataManager = exhibitionDataManager;
        }

        public IActionResult Index()
        {
            SearchViewModel viewModel = new SearchViewModel();

            ArtistModel[] artists = _artistDataManager.GetArtists();
            var artistList = artists.Select(x => new { x.Id, x.FullName }).ToList();
            viewModel.ArtistDropdown = new SelectList(artistList, "Id", "FullName");


            StyleModel[] styles = _artworkDataManager.GetStyles();
            var styleList = styles.Select(x => new { x.Id, x.Style }).ToList();
            viewModel.StyleDropdown = new SelectList(styleList, "Id", "Style");

            var availablityList = new[] {
                new {Id = 0, Availability = "Unavailable"},
                new {Id = 1, Availability = "Available" },
                new {Id = 2, Availability = "Both" }
            }.ToList();

            viewModel.AvailabilityDropdown = new SelectList(availablityList, "Id", "Availability");

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SearchArtists(string name, string surname, string place, string style)
        {
            ArtistModel[] artists = _artistDataManager.SearchArtist(name, surname, place, style);

            Guid[] artistIdList = artists.Select(x => x.Id).ToArray();

            return RedirectToAction("ShowFilteredArtists", "Artist", new { artistsId = artistIdList });
        }

        [HttpPost]
        public IActionResult SearchArtworks(SearchViewModel model)
        {

            ArtistModel artistmodel = null;
            StyleModel stylemodel = null;

            if (model.SelectedArtist != Guid.Empty)
            {
                artistmodel = _artistDataManager.GetOneArtist(model.SelectedArtist);

            }
            if (model.SelectedStyle != Guid.Empty)
            {
                stylemodel = _artworkDataManager.GetOneStyle(model.SelectedStyle);
            }

            ArtworkSearchRequest requestModel = new ArtworkSearchRequest()
            {
                Title = model.Title,
                Artist = artistmodel,
                Style = stylemodel,
                Year = model.Year,
                Type = model.Type,
                Price = model.Price,
                Availability = model.SelectedAvailability
            };

            ArtworkModel[] artworks = _artworkDataManager.SearchArtwork(requestModel);

            Guid[] artworkIdList = artworks.Select(x => x.Id).ToArray();



            return RedirectToAction("ShowFilteredArtworks", "Artwork", new { artworksId = artworkIdList });
        }

        [HttpPost]
        public IActionResult SearchExhibitions(string title, string name, string surname, string artworkTitle, DateTime startDate, DateTime endDate)
        {
            ExhibitionModel[] exhibitions = _exhibitionDataManager.SearchExhibition(title, name, surname, artworkTitle, startDate, endDate);

            Guid[] exhibitionIdList = exhibitions.Select(x => x.Id).ToArray();

            return RedirectToAction("ShowFilteredExhibitions", "Exhibition", new { exhibitionsId = exhibitionIdList });
        }
    }
}
