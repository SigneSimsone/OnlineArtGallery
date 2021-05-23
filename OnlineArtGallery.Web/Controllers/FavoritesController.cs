using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineArtGallery.Web.Data.Managers;
using OnlineArtGallery.Web.Models;

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
            _userManager = userManager;
            _artistDataManager = artistDataManager;
        }

        [Route("favorite-artists")]
        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User).Result;
            ArtistViewModel viewModel = new ArtistViewModel();
            var artists = _artistDataManager.GetFavoriteArtists(user);
            viewModel.Artists = artists;
            viewModel.UserId = user.Id;

            return View("FavoriteArtists", viewModel);
        }
    }
}
