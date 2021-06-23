using Microsoft.AspNetCore.Mvc;
using OnlineArtGallery.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineArtGallery.Web.Data.Managers
{
    public class AdminDataManager : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminDataManager(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public UserModel[] GetUsers()
        {
            var result = _dbContext
                .Users
                .ToArray();

            return result;
        }
        public UserModel GetOneUserByEmail(string email)
        {
            var item = _dbContext.Users.First(x => x.Email == email);

            return item;
        }

        public UserModel GetOneUserById(string id)
        {
            var item = _dbContext.Users.First(x => x.Id == id);

            return item;
        }

        internal void BlockUser(string id, UserModel user)
        {
            var users = _dbContext.Users.First(x => x.Id == id);
            users.IsBlocked = true;

            _dbContext.SaveChanges();
        }

        internal void UnBlockUser(string id, UserModel user)
        {
            var users = _dbContext.Users.First(x => x.Id == id);
            users.IsBlocked = false;

            _dbContext.SaveChanges();
        }
    }
}
