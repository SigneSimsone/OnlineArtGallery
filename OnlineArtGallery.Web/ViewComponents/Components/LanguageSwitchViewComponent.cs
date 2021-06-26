using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineArtGallery.Web.ViewComponents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineArtGallery.Web.ViewComponents.Components
{
    public class LanguageSwitchViewComponent : ViewComponent
    {
        private readonly IOptions<RequestLocalizationOptions> localizationOptions;

    public LanguageSwitchViewComponent(IOptions<RequestLocalizationOptions> localizationOptions)
    {
        this.localizationOptions = localizationOptions;
    }
    public IViewComponentResult Invoke()
    {
        var cultureFeature = HttpContext.Features.Get<IRequestCultureFeature>();
        var model = new LanguageSwitchModel
        {
            SupportedCultures = localizationOptions.Value.SupportedUICultures.ToList(),
            CurrentUICulture = cultureFeature.RequestCulture.UICulture
        };

        return View(model);
    }

}
}
