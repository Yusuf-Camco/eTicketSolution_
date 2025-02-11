using eTicketsApp.Models;

namespace eTicketsApp.Data.Services
{
    public interface IActorServices
    {
        Task<IEnumerable<Actor>> GetAllAsync();
        Actor GetById(int id);
        void Add(Actor actor);
        Actor Update(int id, Actor newActor);
        void Delete(int id);
    }
}
