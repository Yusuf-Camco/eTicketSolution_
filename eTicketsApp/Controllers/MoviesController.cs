using eTicketsApp.Data;
using eTicketsApp.Data.Services.Interfaces;
using eTicketsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace eTicketsApp.Controllers
{

    public class MoviesController : Controller
    {
        private readonly IMovieService _services;
        
        public MoviesController(IMovieService services)
        {
            _services = services;
        }
        public async Task<IActionResult> Index() => View(await _services.GetAllAsync(n => n.Cinema));
        //Get: Producers/Create
        public async Task<IActionResult> Create()
        {
            // Populate the ViewBag with dropdown data
            var dropdownData = await _services.GetNewMovieDropdownValues();
            ViewBag.Cinemas = new SelectList(dropdownData.Cinemas, "Id", "Name");
            ViewBag.Actors = new SelectList(dropdownData.Actors, "Id", "FullName");
            ViewBag.Producers = new SelectList(dropdownData.Producers, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            var dropdownData = await _services.GetNewMovieDropdownValues();
            ViewBag.Cinemas = new SelectList(dropdownData.Cinemas, "Id", "Name");
            ViewBag.Actors = new SelectList(dropdownData.Actors, "Id", "FullName");
            ViewBag.Producers = new SelectList(dropdownData.Producers, "Id", "FullName");
            if (!ModelState.IsValid) return View(movie);
            await _services.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
       
        //Get: Actors/Details/{id}
        public async Task<IActionResult> Details(int id) => View(await _services.GetMovieByIdAsync(id));


        //Get: Actors/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var movieData = await _services.GetMovieByIdAsync(id);
            if (movieData == null)
            {
                return View("Not Found");
            }
            var result = new NewMovieVM()
            {
                Id = movieData.Id,
                Name = movieData.Name,
                Description = movieData.Description,
                StartDate = movieData.StartDate,
                EndDate = movieData.EndDate,
                ImageURL = movieData.ImageURL,
                ProducerId = movieData.ProducerId,
                CinemaId = movieData.CinemaId,
                MovieCategory = movieData.MovieCategory,
                Price = movieData.Price,
                ActorIds = movieData.Actors_Movies.Select(a => a.ActorId).ToList()
                
            };
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM movie)
        {
            if(id != movie.Id)
            {
                return View("Not Found");
            }
            if (!ModelState.IsValid)
            {
                var dropdownData = await _services.GetNewMovieDropdownValues();
                ViewBag.Cinemas = new SelectList(dropdownData.Cinemas, "Id", "Name");
                ViewBag.Actors = new SelectList(dropdownData.Actors, "Id", "FullName");
                ViewBag.Producers = new SelectList(dropdownData.Producers, "Id", "FullName");

                return View(movie);
            };
            await _services.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        //Get: Actors/Delete/{id}
        public async Task<IActionResult> Delete(int id) => View(await _services.GetByIdAsync(id));

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
