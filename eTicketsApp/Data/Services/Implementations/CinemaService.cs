using eTicketsApp.Data.Base;
using eTicketsApp.Data.Services.Interfaces;
using eTicketsApp.Models;
using Microsoft.EntityFrameworkCore;

namespace eTicketsApp.Data.Services.Implementations
{
    public class CinemaService : EntityBaseRepository<Cinema>, ICinemaService
    {
        public CinemaService(AppDbContext context) : base(context) { }
    }
}
