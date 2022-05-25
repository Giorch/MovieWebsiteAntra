using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    //Attribute based routing
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies(int pageSize = 30, int pageNumber = 1)
        {
            var movies = await _movieService.GetMoviesPagination(pageSize, pageNumber);
            return Ok(movies);
        }
        [Route("top-grossing")]
        [HttpGet]
        public async Task<IActionResult> TopGrossing()
        {
            var movies = await _movieService.GetTop30GrossingMovies();
            
            if(movies == null || !movies.Any())
            {
                return NotFound();
            }

            return Ok(movies);
        }

        [HttpGet]
        [Route("top-rated")]
        public async Task<IActionResult> TopRated()
        {

        }

        [HttpGet]
        [Route("genre/{genreId}")]
        public async Task<IActionResult> Genres(int genreId, int pageSize = 30, int pageNumber = 1)
        {

            var pagedMovies = await _movieService.GetMoviesByGenrePagination(genreId, pageSize, pageNumber);

            return Ok(pagedMovies);
        }

        [HttpGet]
        [Route("{id}/reviews")]
        public async Task<IActionResult> Reviews(int id, int pageSize = 30, int pageNumber = 1)
        {
            var pagedReviews = await _movieService.GetReviewsPagination(id, pageSize, pageNumber);

            return Ok(pagedReviews);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            if(movie == null)
            {
                return NotFound(new { ErrorMessage = "No Movie Found" });
            }

            return Ok(movie);
        }
    }
}
