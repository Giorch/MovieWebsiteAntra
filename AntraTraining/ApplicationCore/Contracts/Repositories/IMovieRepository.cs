using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<List<Movie>> GetTop30GrossingMovies();
        //Task<(IEnumerable<Movie>, int totalCount, int totalPages)> GetMoviesByGenres(int genreId, int pageSize = 30, int page = 1);
        Task<PagedResult<Movie>> GetMoviesByGenres(int genreId, int pageSize = 30, int page = 1);

        Task<PagedResult<Movie>> GetAllMovies(int pageSize = 30, int pageNumber = 1);

        Task<PagedResult<Review>> GetReviews(int id, int pageSize = 30, int pageNumber = 1);

        //Task<PagedResult<Movie>> GetMoviesByRating(int pageSize = 30, int page = 1);

    }
}
