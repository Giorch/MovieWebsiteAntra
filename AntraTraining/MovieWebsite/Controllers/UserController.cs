using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieWebsite.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        //[HttpGet]
        // public IActionResult Details(int id)
        // {
        //     return View();
        // }
        // 
        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            //Get user data from cookie
            //var data = this.HttpContext.Request.Cookies["MovieShopAuthCookie"];
            //var isLoggedIn = this.HttpContext.User.Identity.IsAuthenticated;
            //if(!isLoggedIn)
            //{

            //}

            var userId = Convert.ToInt32( this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            return View();
        }
    }
}
