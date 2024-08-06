using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Italy_Recipe_Page.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Italy_Recipe_Page.Controllers
{
    public class UsersController : Controller
    {
        private readonly ItalyContext _context;
        ItalyContext db = new ItalyContext();
        public UsersController(ItalyContext context)
        {
            _context = context;
        }

        public IActionResult Edit()
        {
            var existingUser = db.Users.Find(Int32.Parse(HttpContext.Session.GetString("id")));
            return View();
        }

        [HttpPost]
        public IActionResult Afteredit(User user) {
            
            var existingUser = db.Users.Find(Int32.Parse(HttpContext.Session.GetString("id")));
            if (existingUser != null)
            {
                // Update the properties of the existing user
                string newName = user.Username;
                if (!string.IsNullOrEmpty(newName))
                {
                    existingUser.Username = newName;
                }
                existingUser.Pass = existingUser.Pass;
                existingUser.UserRole = existingUser.UserRole;
                existingUser.RoleStatus = existingUser.RoleStatus;
                existingUser.RoleEndDate = existingUser.RoleEndDate;
                // Only update the Gmail property if a new value is provided
                string newGmail = user.Gmail;
                if (!string.IsNullOrEmpty(newGmail))
                {
                    existingUser.Gmail = newGmail;
                }
                DateOnly d = DateOnly.Parse(HttpContext.Session.GetString("RoleEndDate"));
                if (d!=null)
                {
                    existingUser.RoleEndDate = d;
                }
                // Save the changes to the database
                try
                {
                    db.Entry(existingUser).State = EntityState.Modified;
                    db.SaveChangesAsync();
                    HttpContext.Session.SetString("Username", existingUser.Username.ToString());
                    return View("~/Views/Shared/Success.cshtml");
                }
                catch(Exception e)
                {
                    return View("~/Views/Shared/Fail.cshtml");
                }
            }
            else
            {
                return View("~/Views/Shared/Fail.cshtml");

            }
        }
    }
}
