using eTicketsApp.Models;
using Microsoft.EntityFrameworkCore;

namespace eTicketsApp.Data.Services
{
    public class ActorServices : IActorServices
    {
        private readonly AppDbContext _context;
        public ActorServices(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Actor actor)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Actor>> GetAllAsync()
        {
            var allActors = await _context.Actors.ToListAsync();
            return allActors;
        }

        public Actor GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Actor Update(int id, Actor newActor)
        {
            throw new NotImplementedException();
        }
    }
}
