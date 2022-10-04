using eDoc_APP.Utilities;
using eDoc_Core.Core;
using eDoc_Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace eDoc_APP.Controllers
{
    /// <summary>
    /// Lớp Abstract để kế thừa toàn bộ Controller
    /// </summary>
    [Authorize]
    public abstract class BaseController : Controller
    {
        public readonly eDocumentContext _db;
        public User user;
        public BaseController(eDocumentContext db)
        {
            _db = db;
        }
        public virtual async Task MapMicrosoftAccount()
        {
            string microsoftId = User.GetNameIdentifier();
            string email = User.GetClaimByType("preferred_username");
            string name = User.GetClaimByType("name");
            string username = email.Split('@')?[0];
            var usr = await _db.Users.FirstOrDefaultAsync(x => x.MicrosoftId == microsoftId || x.Username == username);
            if (usr == null)
            {
                usr = new User();
                usr.Username = username;
                usr.Email = email;
                usr.MicrosoftId = microsoftId;
                usr.Fullname = name;
                usr.Birthday = null;
                usr.Password = Extension.RandomString(8).ToScryptEncode();
                _db.Users.Add(usr);
                await _db.SaveChangesAsync();
            }
            user = usr;
        }
        public virtual string GetEmail()
        {
           return User.GetClaimByType("preferred_username");
        }
        public virtual string GetName()
        {
           return User.GetClaimByType("name");
        }
    }
}