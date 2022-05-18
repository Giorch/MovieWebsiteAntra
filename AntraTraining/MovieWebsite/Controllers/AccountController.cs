using ApplicationCore.Contracts.Services;
using ApplicationCore.Exceptions;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace MovieWebsite.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet]
        public IActionResult Register()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            //Save info in user table
            try
            {
                var user = await _accountService.RegisterUser(model);
            }
            catch(ConflictException)
            {
                throw;
            }
            
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            try
            {
                var user = _accountService.LoginUser(model.Email, model.Password);
                if(user != null)
                {
                    return LocalRedirect("~/");

                }
            }
            catch (Exception)
            {
                return View();
                throw;
            }
            return View();
        }

    }
}
