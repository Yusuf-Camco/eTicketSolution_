using eTicketsApp.Data.Base;
using eTicketsApp.Data.Services.Interfaces;
using eTicketsApp.Models;
using Microsoft.EntityFrameworkCore;

namespace eTicketsApp.Data.Services.Implementations
{
    public class ProducerServices : EntityBaseRepository<Producer>, IProducerService
    {
        public ProducerServices(AppDbContext context) : base(context) { }
    }
}
