using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineArtGallery.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineArtGallery.Web.Data.Managers
{
    public class AuditDataManager : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public AuditDataManager(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public AuditModel[] GetAudits()
        {
            var result = _dbContext
                .Audits
                .Include(x => x.User)
                .ToArray();

            return result;
        }
        internal void AddAuditRecord(UserModel user, string action)
        {
            var record = new AuditModel()
            {
                DateTime = DateTime.UtcNow,
                Action = action,
                User = user
            };

            _dbContext.Audits.Add(record);
            _dbContext.SaveChanges();
        }

    }
}
