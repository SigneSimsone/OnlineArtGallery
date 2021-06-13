using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineArtGallery.Web.Data.Managers;
using OnlineArtGallery.Web.Models;
using System;
using System.Linq;

namespace OnlineArtGallery.Web.Controllers
{

    [Authorize]
    public class ArtworkController : Controller
    {
        private readonly ArtworkDataManager _artworkDataManager;
        private readonly ArtistDataManager _artistDataManager;
        private readonly UserManager<UserModel> _userManager;

        public ArtworkController(ArtworkDataManager artworkDataManager, UserManager<UserModel> userManager, ArtistDataManager artistDataManager)
        {
            _artworkDataManager = artworkDataManager;
            _userManager = userManager;
            _artistDataManager = artistDataManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ArtworkModel[] artworks = _artworkDataManager.GetArtworks();
            var userId = _userManager.GetUserId(User);
            ArtworkViewModel viewModel = new ArtworkViewModel();
            viewModel.Artworks = artworks;
            viewModel.UserId = userId;

            ArtistModel[] artists = _artistDataManager.GetArtists();
            var artistList = artists.Select(x => new { x.Id, x.FullName }).ToList();
            viewModel.ArtistDropdown = new SelectList(artistList, "Id", "FullName");


            StyleModel[] styles = _artworkDataManager.GetStyles();
            var styleList = styles.Select(x => new { x.Id, x.Style }).ToList();
            viewModel.StyleDropdown = new SelectList(styleList, "Id", "Style");

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddArtwork(ArtworkViewModel model)
        {
            ArtistModel artistmodel = _artistDataManager.GetOneArtist(model.SelectedArtist);
            StyleModel stylemodel = _artworkDataManager.GetOneStyle(model.SelectedStyle);

            _artworkDataManager.AddArtwork(model.Title, artistmodel, stylemodel, model.Year, model.Description, model.Type, model.Price, model.Availability);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(Guid ArtworkId, ArtworkViewModel model)
        {

            ArtistModel artistmodel = _artistDataManager.GetOneArtist(model.SelectedArtist);
            StyleModel stylemodel = _artworkDataManager.GetOneStyle(model.SelectedStyle);

            _artworkDataManager.Edit(ArtworkId, model.Title, artistmodel, stylemodel, model.Year, model.Description, model.Type, model.Price, model.Availability);


            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(Guid ArtworkId)
        {
            // get artwork from database (ArtworkModel)
            ArtworkModel model = _artworkDataManager.GetOneArtwork(ArtworkId);
            ArtworkViewModel viewModel = new ArtworkViewModel();
            viewModel.Id = model.Id;
            viewModel.Title = model.Title;

            ArtistModel[] artists = _artistDataManager.GetArtists();
            var artistList = artists.Select(x => new { x.Id, x.FullName }).ToList();

            viewModel.ArtistDropdown = new SelectList(artistList, "Id", "FullName", model.Artist.FullName);


            StyleModel[] styles = _artworkDataManager.GetStyles();
            var styleList = styles.Select(x => new { x.Id, x.Style }).ToList();

            viewModel.StyleDropdown = new SelectList(styleList, "Id", "Style", model.Style.Style);

            viewModel.Year = model.Year;
            viewModel.Description = model.Description;
            viewModel.Type = model.Type;
            viewModel.Price = model.Price;
            viewModel.Availability = model.Availability;

            // return view 

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Favorite(Guid ArtworkId)
        {
            var user = _userManager.GetUserAsync(User).Result;

            _artworkDataManager.FavoriteArtwork(ArtworkId, user);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Unfavorite(Guid ArtworkId)
        {
            var user = _userManager.GetUserAsync(User).Result;

            _artworkDataManager.UnFavoriteArtwork(ArtworkId, user);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(Guid ArtworkId)
        {
            _artworkDataManager.Delete(ArtworkId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult OpenArtwork(Guid ArtworkId)
        {
            // get artwork from database (ArtworkModel)
            ArtworkModel model = _artworkDataManager.GetOneArtwork(ArtworkId);

            return View("OneArtwork", model);
        }
        [HttpGet]
        public IActionResult BuyArtwork(Guid ArtworkId)
        {
            // get artwork from database (ArtworkModel)
            ArtworkModel model = _artworkDataManager.GetOneArtwork(ArtworkId);

            return View("OrderArtwork", model);
        }

        [HttpPost]
        public IActionResult AddOrder(string address, Guid ArtworkId)
        {
            ArtworkModel artworkmodel = _artworkDataManager.GetOneArtwork(ArtworkId);

            var userId = _userManager.GetUserId(User);
            UserModel usermodel = _artworkDataManager.GetOneUser(userId);

            _artworkDataManager.AddOrder(address, artworkmodel, usermodel);
            _artworkDataManager.EditAvailabilityAfterBuying(ArtworkId);

            HomeModel homemodel = new HomeModel();
            homemodel.Message = "Your Order has been accepted!";

            return RedirectToAction("Index", "Home", homemodel);
        }



        [HttpPost]
        public IActionResult AddFeedback(Guid ArtworkId, string comment)
        {
            var user = _userManager.GetUserAsync(User).Result;
            _artworkDataManager.AddFeedback(ArtworkId, comment, user);

            return RedirectToAction(nameof(OpenArtwork), new { Id = ArtworkId });
        }

        [HttpPost]
        public IActionResult DeleteFeedback(Guid FeedbackId, Guid ArtworkId)
        {
            _artworkDataManager.DeleteFeedback(FeedbackId);

            return RedirectToAction(nameof(OpenArtwork), new { Id = ArtworkId });
        }




        [HttpPost]
        public IActionResult AddStyle(string style)
        {
            _artworkDataManager.AddStyle(style);

            return RedirectToAction(nameof(Index));
        }

    }
}

