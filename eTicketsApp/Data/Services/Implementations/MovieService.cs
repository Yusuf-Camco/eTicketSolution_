using eTicketsApp.Data.Base;
using eTicketsApp.Data.Services.Interfaces;
using eTicketsApp.Models;
using Microsoft.EntityFrameworkCore;

namespace eTicketsApp.Data.Services.Implementations
{
    public class MovieService : EntityBaseRepository<Movie>, IMovieService
    {
        private readonly AppDbContext _context;
        public MovieService(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movie = await _context.Movies
                                .Include(c => c.Cinema)
                                .Include(p => p.Producer)
                                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                                .FirstOrDefaultAsync(a => a.Id == id);



            // Debug: Check if related data is loaded
            Console.WriteLine($"Cinema: {movie.Cinema?.Name}");
            Console.WriteLine($"Producer: {movie.Producer?.FullName}");
            foreach (var actorMovie in movie.Actors_Movies)
            {
                Console.WriteLine($"Actor: {actorMovie.Actor?.FullName}");
            }

            return movie;
        }
    }
}
