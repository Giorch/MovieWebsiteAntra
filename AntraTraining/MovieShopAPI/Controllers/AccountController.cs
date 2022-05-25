using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await _accountService.RegisterUser(model);

            return Ok(user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();

            }
                var user = await _accountService.LoginUser(model.Email, model.Password); 
                return Ok(user);
        }

        [HttpGet]
        [Route("check-email")]
        public async Task<IActionResult> CheckEmail(string email)
        {
            var check = await _accountService.CheckEmail(email);
            return Ok(check);
        }
    }
}
