using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;

        public ActorsController( IActorsService service)
        {
            _service = service;
        }


        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var ActorsList= await _service.GetAllAsync();
            return View( ActorsList);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(Actor actor) { 

            if (!ModelState.IsValid)
            {
             return View(actor);

            }
            
            await _service.AddAsync(actor);
            return RedirectToAction("Index");


        }

        //Actors/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {

            var actorDetails= await _service.GetByIdAsync(id);

            if (actorDetails == null)
            {
                return View("NotFound");
            }
            return View(actorDetails);
        }

        //Actors/Edit/1
        public async Task<IActionResult> Edit (int id)
        {
            var actorDetails= await _service.GetByIdAsync(id);
            if (actorDetails == null)
            {
                return View("NotFound");
            }
            return View(actorDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Actor actor) 
        {
            if (!ModelState.IsValid)
            {
                return View(actor);

            }

            await _service.UpdateAsync(id, actor);
            return RedirectToAction("Index");

        }


        public async Task<IActionResult> Delete(int id)
        {
            
                var actorDetails = await _service.GetByIdAsync(id);
                if (actorDetails == null)
                {
                    return View("NotFound");
                }
                return View(actorDetails);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!ModelState.IsValid)
            {
                var actorDetails = await _service.GetByIdAsync(id);
                if (actorDetails == null)
                {
                    return View("NotFound");
                }
                

            }

            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
