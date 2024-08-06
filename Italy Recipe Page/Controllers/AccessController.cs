using Microsoft.AspNetCore.Mvc;
using Italian_Restaurant_1.Models;
using Italy_Recipe_Page.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Italian_Restaurant_1.Controllers
{
    public class AccessController : Controller
	{
		ItalyContext db = new ItalyContext();
        [HttpGet]
        public ActionResult Login()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
				return View();						
            }
            else
            {
				return RedirectToAction("Index", "Home");
			}
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                var u = db.Users.Where(x => x.Username.Equals(user.Username) && x.Pass.Equals(user.Pass)).FirstOrDefault();
                
                if (u!=null)
                {   
					HttpContext.Session.SetString("id", u.UserId.ToString());
					HttpContext.Session.SetString("Username", u.Username.ToString());
					HttpContext.Session.SetString("Pass", u.Pass.ToString());
					HttpContext.Session.SetString("UserRole", u.UserRole.ToString());
                    HttpContext.Session.SetString("RoleEndDate", u.RoleEndDate.ToString());
                    if (u.Gmail != null)
                    {
                        HttpContext.Session.SetString("Gmail", u.Gmail.ToString());
                    }
                    if (string.Equals(u.UserRole.ToString(), "2", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    
                    else
                    {
                        return View("~/Views/Shared/in.cshtml");
                    }
                }
                
            }
            return View("~/Views/Shared/infail.cshtml");
        }

        public IActionResult SignUp(User user)
        {
            User x = new User
            {   
                Username = user.Username,
                Pass = user.Pass,   
                UserRole = "0",
                RoleStatus = "0",
                RoleEndDate = DateOnly.FromDateTime(DateTime.Now)
            };

            if (ModelState.IsValid)
            {
                try
                {
                    db.Users.Add(x);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    return View("~/Views/Shared/fail.cshtml");
                }
            }
            
            return View("~/Views/Shared/Success.cshtml");
        }

        public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			HttpContext.Session.Remove("UserName");
			return RedirectToAction("Index", "Home");
		}
	}
}
