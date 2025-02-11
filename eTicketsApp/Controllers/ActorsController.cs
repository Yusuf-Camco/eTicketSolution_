using eTicketsApp.Data;
using eTicketsApp.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTicketsApp.Controllers
{
    
    public class ActorsController : Controller
    {
        private readonly IActorServices _services;
        public ActorsController(IActorServices services)
        {
            _services = services;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _services.GetAllAsync();
            return View(data);
        }

        //Get: Actors/Create
        public IActionResult Create()
        {
            return View();
        }
    }
}
