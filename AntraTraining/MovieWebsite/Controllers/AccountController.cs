﻿using ApplicationCore.Contracts.Services;
using ApplicationCore.Exceptions;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
                var user = await _accountService.LoginUser(model.Email, model.Password);
                if(user != null)
                {

                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.GivenName, user.FirstName),
                        new Claim(ClaimTypes.Surname, user.LastName),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToShortDateString()),
                        
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));



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
