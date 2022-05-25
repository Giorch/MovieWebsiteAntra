using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IUserService
    {
        Task AddFavorite(FavoriteRequestModel favorite);
        Task RemoveFavorite(FavoriteRequestModel favorite);
        Task<bool> FavoriteExists(int id, int movieId);
        Task<List<MovieCardModel>>GetAllFavorites(int userId);
    }
}
