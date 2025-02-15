using eTicketsApp.Data.Base;
using eTicketsApp.Data.Services.Interfaces;
using eTicketsApp.Data.ViewModel;
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

        public async Task AddNewMovieAsync(NewMovieVM newMovie)
        {
            var newMovieData = new Movie()
            {
                Name = newMovie.Name,
                Description = newMovie.Description,
                ImageURL = newMovie.ImageURL,
                StartDate = newMovie.StartDate,
                EndDate = newMovie.EndDate,
                Price = newMovie.Price,
                MovieCategory = newMovie.MovieCategory,
                ProducerId = newMovie.ProducerId,
                CinemaId = newMovie.CinemaId,
            };
            await _context.AddAsync(newMovieData);
            await _context.SaveChangesAsync();

            foreach (var actorId in newMovie.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovieData.Id,
                    ActorId = actorId,
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movie = await _context.Movies
                                .Include(c => c.Cinema)
                                .Include(p => p.Producer)
                                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                                .FirstOrDefaultAsync(a => a.Id == id);

            return movie;
        }

        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownValues()
        {
            var response = new NewMovieDropdownsVM()
            {
                Actors = await _context.Actors.OrderBy(a => a.FullName).ToListAsync(),
                Producers = await _context.Producers.OrderBy(a => a.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(a => a.Name).ToListAsync()
            };

            return response;
        }

        public async Task UpdateMovieAsync(NewMovieVM newMovie)
        {
            var result = await _context.Movies.FirstOrDefaultAsync(m => m.Id == newMovie.Id);

            if (result != null)
            {
                result.Name = newMovie.Name;
                result.Description = newMovie.Description;
                result.ImageURL = newMovie.ImageURL;
                result.StartDate = newMovie.StartDate;
                result.EndDate = newMovie.EndDate;
                result.Price = newMovie.Price;
                result.MovieCategory = newMovie.MovieCategory;
                result.ProducerId = newMovie.ProducerId;
                result.CinemaId = newMovie.CinemaId;
                await _context.SaveChangesAsync();
            }
            

            var existingActors = _context.Actors_Movies.Where(n => n.MovieId == newMovie.Id).ToList();
            _context.RemoveRange(existingActors);
            await _context.SaveChangesAsync();

            foreach (var actorId in newMovie.ActorIds) 
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                _context.AddRange(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }
    }
}
