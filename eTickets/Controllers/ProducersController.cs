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
    public class ProducersController : Controller
    {

        private readonly IProducersService _service;

        public ProducersController( IProducersService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var ProducersList= await _service.GetAllAsync();
            return View(ProducersList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producer producer)
        {

            if (!ModelState.IsValid)
            {
                return View(producer);

            }

            await _service.AddAsync(producer);
            return RedirectToAction("Index");


        }

        //Actors/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {

            var producerDetails = await _service.GetByIdAsync(id);

            if (producerDetails == null)
            {
                return View("NotFound");
            }
            return View(producerDetails);
        }

        //GET:
        //Producers/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null)
            {
                return View("NotFound");
            }
            return View(producerDetails);
        }

        //POST
        ////Producers/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);

            }

            await _service.UpdateAsync(id, producer);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(int id)
        {

            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null)
            {
                return View("NotFound");
            }
            return View(producerDetails);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!ModelState.IsValid)
            {
                var producerDetails = await _service.GetByIdAsync(id);
                if (producerDetails == null)
                {
                    return View("NotFound");
                }


            }

            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}

