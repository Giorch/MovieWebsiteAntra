using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MovieWebsite.Models;
using Infrastructure.Services;
using ApplicationCore.Contracts.Services;

namespace MovieWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;
        public HomeController(ILogger<HomeController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        [HttpGet]
        public IActionResult Index()
        {

            //var movieService = new MovieService();
            var movieCards = _movieService.GetTop30GrossingMovies();
            // passing the data from Controller action method to View
            return View(movieCards);
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        // https://localhost:7273/home/TopRatedMovies
        public IActionResult TopRatedMovies()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}