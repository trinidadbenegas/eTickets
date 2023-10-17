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
    public class CinemasController : Controller
    {

        private readonly ICinemasService _service;

        public CinemasController(ICinemasService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var CinemasList= await _service.GetAllAsync();
            return View(CinemasList);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cinema cinema)
        {

            if (!ModelState.IsValid)
            {
                return View(cinema);

            }

            await _service.AddAsync(cinema);
            return RedirectToAction("Index");


        }

        //Actors/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {

            var cinemaDetails = await _service.GetByIdAsync(id);

            if (cinemaDetails == null)
            {
                return View("NotFound");
            }
            return View(cinemaDetails);
        }

        //Actors/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null)
            {
                return View("NotFound");
            }
            return View(cinemaDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);

            }

            await _service.UpdateAsync(id, cinema);
            return RedirectToAction("Index");

        }


        public async Task<IActionResult> Delete(int id)
        {

            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null)
            {
                return View("NotFound");
            }
            return View(cinemaDetails);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!ModelState.IsValid)
            {
                var cinemaDetails = await _service.GetByIdAsync(id);
                if (cinemaDetails == null)
                {
                    return View("NotFound");
                }


            }

            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
