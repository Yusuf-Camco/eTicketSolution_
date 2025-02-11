using eTicketsApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTicketsApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AppDbContext _context;
        public MoviesController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Models.Movie> data = await _context.Movies.Include(n => n.Cinema).ToListAsync();
            return View(data);
        }
    }
}
