using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CastRepository : Repository<Cast>, ICastRepository
    {
        public CastRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Cast> GetById(int Id)
        {
            //var cast = await _dbContext.Movies.Include(m => m.MoviesOfGenres).ThenInclude(m => m.Genre).Include(m => m.CastOfMovie).ThenInclude(m => m.Cast).Include(m => m.Trailers).FirstOrDefaultAsync(m => m.Id == Id);
            var cast = await _dbContext.Casts.Include(c => c.CastOfMovie).ThenInclude(c => c.Movie).FirstOrDefaultAsync(c => c.Id == Id);
            return cast;
        }
    }
}
