using eTicketsApp.Data.Base;
using eTicketsApp.Data.Services.Interfaces;
using eTicketsApp.Models;
using Microsoft.EntityFrameworkCore;

namespace eTicketsApp.Data.Services.Implementations
{
    public class ActorService : EntityBaseRepository<Actor>, IActorService
    {
        public ActorService(AppDbContext context) : base(context) { }
    }
}
