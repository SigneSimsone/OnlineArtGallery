using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineArtGallery.Web.Data.Managers;
using OnlineArtGallery.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineArtGallery.Web.Controllers
{
    public class ExhibitionController : Controller
    {
        private readonly ExhibitionDataManager _exhibitionDataManager;
        private readonly ArtworkDataManager _artworkDataManager;
        private readonly ArtistDataManager _artistDataManager;

        public ExhibitionController(ExhibitionDataManager exhibitionDataManager, ArtworkDataManager artworkDataManager, ArtistDataManager artistDataManager)
        {
            _exhibitionDataManager = exhibitionDataManager;
            _artworkDataManager = artworkDataManager;
            _artistDataManager = artistDataManager;
        }

        public IActionResult Index()
        {
            ExhibitionModel[] exhibitions = _exhibitionDataManager.GetExhibitions();
            ExhibitionViewModel viewModel = new ExhibitionViewModel();
            viewModel.Exhibitions = exhibitions;

            ArtworkModel[] artworks = _artworkDataManager.GetArtworks();
            var artworkList = artworks.Select(x => new ArtworkArtistName(x.Id, $"Title: {x.Title} Artist: {x.Artist.FullName}")).ToList();
            viewModel.ArtworkDropdown = new MultiSelectList(artworkList, "Id", "Name");

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult ShowFilteredExhibitions(Guid[] exhibitionsId)
        {
            ExhibitionModel[] exhibitions = _exhibitionDataManager.GetExhibitions(exhibitionsId.ToList());

            ExhibitionViewModel viewModel = new ExhibitionViewModel();
            viewModel.Exhibitions = exhibitions;

            ArtworkModel[] artworks = _artworkDataManager.GetArtworks();
            var artworkList = artworks.Select(x => new ArtworkArtistName(x.Id, $"Title: {x.Title} Artist: {x.Artist.FullName}")).ToList();
            viewModel.ArtworkDropdown = new MultiSelectList(artworkList, "Id", "Name");

            return View("Index", viewModel);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddExhibition(ExhibitionViewModel model)
        {
            ArtworkModel[] artworkmodel = _artworkDataManager.GetArtworksForExhibition(model.SelectedArtworks);
            List<ArtistModel> artists = new List<ArtistModel>();
            foreach (var artwork in artworkmodel)
            {
                var artist = artwork.Artist;
                artists.Add(artist);
            }

            _exhibitionDataManager.AddExhibition(model.Title, artists.ToArray(), artworkmodel, model.StartDate, model.EndDate);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(Guid ExhibitionId, ExhibitionViewModel model)
        {

            ArtworkModel[] artworkmodel = _artworkDataManager.GetArtworksForExhibition(model.SelectedArtworks);
            List<ArtistModel> artists = new List<ArtistModel>();
            foreach (var artwork in artworkmodel)
            {
                var artist = artwork.Artist;
                artists.Add(artist);
            }

            _exhibitionDataManager.Edit(ExhibitionId, model.Title, artists.ToArray(), artworkmodel, model.StartDate, model.EndDate);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(Guid ExhibitionId)
        {
            ExhibitionModel model = _exhibitionDataManager.GetOneExhibition(ExhibitionId);
            ExhibitionViewModel viewModel = new ExhibitionViewModel();
            viewModel.Id = model.Id;
            viewModel.Title = model.Title;

            ArtworkModel[] artworks = _artworkDataManager.GetArtworks();
            var artworkList = artworks.Select(x => new ArtworkArtistName(x.Id, $"Title: {x.Title} Artist: {x.Artist.FullName}")).ToList();
            viewModel.ArtworkDropdown = new MultiSelectList(artworkList, "Id", "Name");

            viewModel.StartDate = model.StartDate;
            viewModel.EndDate = model.EndDate;

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult OpenExhibition(Guid ExhibitionId)
        {

            ExhibitionModel model = _exhibitionDataManager.GetOneExhibition(ExhibitionId);

            return View("OneExhibition", model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(Guid ExhibitionId)
        {
            _exhibitionDataManager.Delete(ExhibitionId);

            return RedirectToAction(nameof(Index));
        }
    }
}
