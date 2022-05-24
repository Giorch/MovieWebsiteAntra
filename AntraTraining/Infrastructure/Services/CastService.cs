using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;

        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }

        public async Task<CastDetailsModel> GetCastDetails(int CastId)
        {
            var cast = await _castRepository.GetById(CastId);
            var castDetails = new CastDetailsModel
            {
                Id = cast.Id,
                Name = cast.Name,
                ProfilePath = cast.ProfilePath,
                Gender = cast.Gender,
                TmdbUrl = cast.TmdbUrl
            };
            foreach(var movie in cast.CastOfMovie)
            {
                castDetails.MoviesOfCast.Add(new MovieCardModel
                {
                    Id = movie.Movie.Id,
                    Title = movie.Movie.Title,
                    PosterUrl = movie.Movie.PosterUrl
                }); 
            }
            return castDetails;

        }
    }
}
