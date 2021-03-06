using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IMovieService
    {

        Task<List<MovieCardModel>> GetTop30GrossingMovies();
        Task<MovieDetailsModel> GetMovieDetails(int movieId);
        Task<PagedResult<MovieCardModel>> GetMoviesByGenrePagination(int genreId, int pageSize = 30, int pageNumber = 1);
        Task<PagedResult<MovieCardModel>> GetMoviesPagination(int pageSize = 30, int pageNumber = 1);
        Task<PagedResult<ReviewModel>>GetReviewsPagination(int id, int pageSize = 30, int pageNumber = 1);
    }
}
