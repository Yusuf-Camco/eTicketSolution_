using eTicketsApp.Data;
using eTicketsApp.Data.Services.Interfaces;
using eTicketsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTicketsApp.Controllers
{

    public class ActorsController : Controller
    {
        private readonly IActorService _services;
        public ActorsController(IActorService services)
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

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName", "ProfilePictureURL", "Bio")]Actor actor)
        {
            if(!ModelState.IsValid) return View(actor);
            await _services.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        //Get: Actors/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var actorToGet = await _services.GetByIdAsync(id);
            if(actorToGet == null) return View("Not Found");
            return View(actorToGet);
        }

        //Get: Actors/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var actorToGet = await _services.GetByIdAsync(id);
            if (actorToGet == null) return View("Not Found");
            return View(actorToGet);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Actor actor)
        {
            if (!ModelState.IsValid) return View(actor);
            await _services.UpdateAsync(id, actor);
            return RedirectToAction(nameof(Index));
        }

        //Get: Actors/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var actorToGet = await _services.GetByIdAsync(id);
            if (actorToGet == null) return View("Not Found");
            return View(actorToGet);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorToGet = await _services.GetByIdAsync(id);
            if (actorToGet == null) return View("Not Found");
            await _services.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
