using eTicketsApp.Data;
using eTicketsApp.Data.Services.Interfaces;
using eTicketsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTicketsApp.Controllers
{

    public class MoviesController : Controller
    {
        private readonly IMovieService _services;
        public MoviesController(IMovieService services)
        {
            _services = services;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _services.GetAllAsync(n => n.Cinema);
            return View(data);
        }

        //Get: Producers/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name", "ImageURL", "Description")]Movie movie)
        {
            if(!ModelState.IsValid) return View(movie);
            await _services.AddAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        //Get: Actors/Details/{id}
        public async Task<IActionResult> Details(int id) => View(await _services.GetMovieByIdAsync(id));
        

        //Get: Actors/Edit/{id}
        public async Task<IActionResult> Edit(int id) => View(await _services.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Movie movie)
        {
            if (!ModelState.IsValid) return View(movie);
            await _services.UpdateAsync(id, movie);
            return RedirectToAction(nameof(Index));
        }

        //Get: Actors/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _services.GetByIdAsync(id);
            if (result == null) return View("Not Found");
            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _services.GetByIdAsync(id);
            if (result == null) return View("Not Found");
            await _services.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
