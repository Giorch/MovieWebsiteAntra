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

        public MovieDetailsModel GetMovieDetails(int movieId)
        {
            var movie = _movieRepository.GetById(movieId);
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

        public List<MovieCardModel> GetTop30GrossingMovies()
        {
            // call the movierepository class
            // get the entity class data and map them in to model class data
            //var movieRepo = new MovieRepository();
            var movies =  _movieRepository.GetTop30GrossingMovies();

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
