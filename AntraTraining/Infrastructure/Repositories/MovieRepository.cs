using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<Movie>> GetTop30GrossingMovies()
        {
            // SQL Database 
            // data access logic
            // ADO.NET (Microsoft) SQLConnection, SQLCommand
            // Dapper (ORM) -> StackOverflow
            // Entity Framework Core => LINQ
            // SELECT top 30 * from Movie order by Revenue
            // movies.orderbydescnding(m=> m.Revenue).Take(30)

            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }

        public override async Task<Movie> GetById(int Id)
        {
            var movie = await _dbContext.Movies.Include(m => m.MoviesOfGenres).ThenInclude(m => m.Genre).Include(m => m.CastOfMovie).ThenInclude(m => m.Cast).Include(m => m.Trailers).FirstOrDefaultAsync(m => m.Id == Id);
            
            return movie;
            //FirstOrDefault doesn't throw exception
            //First throws exception if no resultrs found
            //SingleOrDefault throws exception if there are multiple results
            //Single throws exception if there is not a single result
        }

        public async Task<PagedResult<Movie>> GetMoviesByGenres(int genreId, int pageSize = 30, int pageNumber = 1)
        {
            //get total movies count for that genre
            var totalMoviesCountByGenre = await _dbContext.MovieGenres.Where(m => m.GenreId == genreId).CountAsync();
            //get the actual movies from MovieGenre and Movie table
            if(totalMoviesCountByGenre == 0)
            {
                throw new Exception("No movies found for this genre");

            }
            var movies = await _dbContext.MovieGenres.Where(g => g.GenreId == genreId).Include(m => m.Movie).OrderBy(m => m.MovieId).Select(m=>new Movie
            {
                Id = m.MovieId,
                PosterUrl = m.Movie.PosterUrl,
                Title = m.Movie.Title,

            }).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var pagedMovies = new PagedResult<Movie>(movies, pageNumber, pageSize, totalMoviesCountByGenre);
            return pagedMovies;
        }
       
    }
}
