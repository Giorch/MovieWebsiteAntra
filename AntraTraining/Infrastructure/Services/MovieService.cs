using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<MovieDetailsModel> GetMovieDetails(int movieId)
        {
            var movie = await _movieRepository.GetById(movieId);

            if(movie == null)
            {
                return null;
            }
            var movieDetails = new MovieDetailsModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Budget = movie.Budget,
                Overview = movie.Overview,
                Price = movie.Price,
                PosterUrl = movie.PosterUrl,
                Revenue = movie.Revenue,
                ReleaseDate = movie.ReleaseDate,
                Tagline = movie.Tagline,
                RunTime = movie.RunTime,
                BackdropUrl = movie.BackdropUrl,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl
                

            };
            foreach(var trailer in movie.Trailers)
            {
                movieDetails.Trailers.Add(new TrailerModel { Id = trailer.Id, Name = trailer.Name, 
                    TrailerUrl = trailer.TrailerUrl  });
            }
            foreach(var genre in movie.MoviesOfGenres)
            {
                movieDetails.Genres.Add(new GenreModel { Id = genre.GenreId, Name = genre.Genre.Name });

            }
            foreach(var cast in movie.CastOfMovie)
            {
                movieDetails.Casts.Add(new CastModel { Id = cast.CastId, Name = cast.Cast.Name, Character = cast.Character,
                    ProfilePath = cast.Cast.ProfilePath });
            }
            return movieDetails;
        }

        public async Task<PagedResult<MovieCardModel>> GetMoviesByGenrePagination(int genreId, int pageSize = 30, int pageNumber = 1)
        {
            var pagedMovies = await _movieRepository.GetMoviesByGenres(genreId, pageSize, pageNumber);
            var movieCards = new List<MovieCardModel>();
            movieCards.AddRange(pagedMovies.Data.Select(x => new MovieCardModel { 
                Id = x.Id,
                PosterUrl = x.PosterUrl,
                Title = x.Title 
            }));

            return new PagedResult<MovieCardModel>(movieCards, pageNumber, pageSize, pagedMovies.Count);


        }
        public async Task<PagedResult<MovieCardModel>> GetMoviesPagination(int pageSize = 30, int pageNumber = 1)
        {
            var pagedMovies = await _movieRepository.GetAllMovies(pageSize, pageNumber);
            var moviesCards = new List<MovieCardModel>();
            moviesCards.AddRange(pagedMovies.Data.Select(x => new MovieCardModel
            {
                Id = x.Id,
                PosterUrl = x.PosterUrl,
                Title = x.Title
            }));

            return new PagedResult<MovieCardModel>(moviesCards, pageNumber, pageSize, pagedMovies.Count);
        }

        public async Task<PagedResult<ReviewModel>> GetReviewsPagination(int id, int pageSize = 30, int pageNumber = 1)
        {
            var pagedReviews = await _movieRepository.GetReviews(id, pageSize, pageNumber);
            var reviewList = new List<ReviewModel>();
            reviewList.AddRange(pagedReviews.Data.Select(x => new ReviewModel
            {
                MovieId = x.MovieId,
                UserId = x.UserId,
                ReviewText = x.ReviewText,
                Rating = x.Rating
            }));
            return new PagedResult<ReviewModel>(reviewList, pageNumber, pageSize, pagedReviews.Count);
        }

        public async Task<List<MovieCardModel>> GetTop30GrossingMovies()
        {
            // call the movierepository class
            // get the entity class data and map them in to model class data
            //var movieRepo = new MovieRepository();
            var movies = await _movieRepository.GetTop30GrossingMovies();

            var movieCards = new List<MovieCardModel>();

            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    Title = movie.Title
                });
            }

            return movieCards;
        }
    }
}
