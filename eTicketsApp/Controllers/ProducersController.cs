using eTicketsApp.Data;
using eTicketsApp.Data.Services.Interfaces;
using eTicketsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTicketsApp.Controllers
{

    public class ProducersController : Controller
    {
        private readonly IProducerService _services;
        public ProducersController(IProducerService services)
        {
            _services = services;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _services.GetAllAsync();
            return View(data);
        }

        //Get: Producers/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName", "ProfilePictureURL", "Bio")]Producer producer)
        {
            if(!ModelState.IsValid) return View(producer);
            await _services.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }

        //Get: Actors/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var result = await _services.GetByIdAsync(id);
            if(result == null) return View("Not Found");
            return View(result);
        }

        //Get: Actors/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _services.GetByIdAsync(id);
            if (result == null) return View("Not Found");
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Producer producer)
        {
            if (!ModelState.IsValid) return View(producer);
            await _services.UpdateAsync(id, producer);
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
