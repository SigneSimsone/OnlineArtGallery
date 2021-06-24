using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineArtGallery.Web.Data.Managers;
using OnlineArtGallery.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineArtGallery.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AuditController : Controller
    {
        private readonly AuditDataManager _auditDataManager;

        public AuditController(AuditDataManager auditDataManager)
        {
            _auditDataManager = auditDataManager;
        }

        public IActionResult Index()
        {
            AuditModel[] audits = _auditDataManager.GetAudits();
            
            AuditViewModel viewModel = new AuditViewModel();
            viewModel.Audits = audits;

            return View(viewModel);
        }
    }
}
