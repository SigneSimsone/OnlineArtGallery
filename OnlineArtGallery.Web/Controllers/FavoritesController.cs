using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineArtGallery.Web.Data.Managers;
using OnlineArtGallery.Web.Models;
using System;

namespace OnlineArtGallery.Web.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly ArtworkDataManager _artworkDataManager;
        private readonly ArtistDataManager _artistDataManager;
        private readonly UserManager<UserModel> _userManager;

        public FavoritesController(ArtworkDataManager artworkDataManager, UserManager<UserModel> userManager, ArtistDataManager artistDataManager)
        {
            _artworkDataManager = artworkDataManager;
            _artistDataManager = artistDataManager;
            _userManager = userManager;
        }

        [Route("favorite-artists")]
        public IActionResult FavoriteArtists()
        {
            var user = _userManager.GetUserAsync(User).Result;
            ArtistViewModel viewModel = new ArtistViewModel();
            var artists = _artistDataManager.GetFavoriteArtists(user);
            viewModel.Artists = artists;
            viewModel.UserId = user.Id;

            return View("FavoriteArtists", viewModel);
        }

        [Route("favorite-artworks")]
        public IActionResult FavoriteArtworks()
        {
            var user = _userManager.GetUserAsync(User).Result;
            ArtworkViewModel viewModel = new ArtworkViewModel();
            var artworks = _artworkDataManager.GetFavoriteArtworks(user);
            viewModel.Artworks = artworks;
            viewModel.UserId = user.Id;

            /*foreach (var artwork in viewModel.Artworks){
                if (artwork.Users.Any(x => x.Id == viewModel.UserId)){}}*/

            return View("FavoriteArtworks", viewModel);
        }

        [HttpPost]
        public IActionResult UnfavoriteArtist(Guid id)
        {
            var user = _userManager.GetUserAsync(User).Result;

            _artistDataManager.UnFavoriteArtist(id, user);

            return RedirectToAction(nameof(FavoriteArtists));
        }

        [HttpPost]
        public IActionResult UnfavoriteArtwork(Guid id)
        {
            var user = _userManager.GetUserAsync(User).Result;

            _artworkDataManager.UnFavoriteArtwork(id, user);

            return RedirectToAction(nameof(FavoriteArtworks));
        }

        [HttpGet]
        public IActionResult OpenArtist(Guid Id)
        {
            return RedirectToAction("OpenArtist", "Artist", new { ArtistId = Id });
        }

        [HttpGet]
        public IActionResult OpenArtwork(Guid Id)
        {
            return RedirectToAction("OpenArtwork", "Artwork", new { ArtworkId = Id });
        }
    }
}
