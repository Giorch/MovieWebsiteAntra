using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }
        public List<Movie> GetTop30GrossingMovies()
        {
            // SQL Database 
            // data access logic
            // ADO.NET (Microsoft) SQLConnection, SQLCommand
            // Dapper (ORM) -> StackOverflow
            // Entity Framework Core => LINQ
            // SELECT top 30 * from Movie order by Revenue
            // movies.orderbydescnding(m=> m.Revenue).Take(30)

            var movies = _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToList();
            return movies;
        }

        public override Movie GetById(int Id)
        {
            var movie = _dbContext.Movies.Include(m => m.MoviesOfGenres).ThenInclude(m => m.Genre).Include(m=>m.CastOfMovie).ThenInclude(m=>m.Cast).Include(m=>m.Trailers).FirstOrDefault(m => m.Id == Id);

            return movie;
            //FirstOrDefault doesn't throw exception
            //First throws exception if no resultrs found
            //SingleOrDefault throws exception if there are multiple results
            //Single throws exception if there is not a single result
        }
    }
}
