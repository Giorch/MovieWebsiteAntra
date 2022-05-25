using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddFavorite(FavoriteRequestModel favorite)
        {
            var user = await _userRepository.GetById(favorite.UserId);
            user.Favorites.Add(new Favorite { UserId = favorite.UserId, MovieId = favorite.MovieId, User = favorite.User, Movie = favorite.Movie });

        }

        public async Task<bool> FavoriteExists(int userId, int movieId)
        {
            var user = await _userRepository.GetById(userId);
            foreach (var favorite in user.Favorites)
            {
                if(favorite.MovieId == movieId)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<List<MovieCardModel>> GetAllFavorites(int userId)
        {
            var user = await _userRepository.GetById(userId);
            var movies = new List<MovieCardModel>();
            if (user.Favorites != null)
            {

                foreach (var movie in user.Favorites)
                {
                    movies.Add(new MovieCardModel { Id = movie.Movie.Id, Title = movie.Movie.Title, PosterUrl = movie.Movie.PosterUrl });
                }
            }
            return movies;
        }

        public async Task RemoveFavorite(FavoriteRequestModel favorite)
        {
            var user = await _userRepository.GetById(favorite.UserId);
            user.Favorites.Remove(new Favorite { UserId = favorite.UserId, MovieId = favorite.MovieId, User = favorite.User, Movie = favorite.Movie });
        }
    }
}
