using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MovieWebsite.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<IActionResult> Details(int id)
        {
            //Data
            //Remote Database
            //CPU bound operation => PI, Loan calculations
            //I/O bound operation => Database calls, file, images, videos. Async preferred

            //External thread speed dependencies:
            //Network, SQL server Query, Server Memory, (etc)
            //T1 is just waiting so it's available for a new request
            //Basically multithreading
            var movie = await _movieService.GetMovieDetails(id);
            return View(movie);
        }
        [HttpGet]
        public async Task<IActionResult> Genres(int id, int pageSize=30, int pageNumber =1)
        {

            var pagedMovies = await _movieService.GetMoviesByGenrePagination(id, pageSize, pageNumber);

            return View("PagedMovies", pagedMovies);
        }
    }
}
