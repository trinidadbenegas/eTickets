using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;

        public MoviesController(IMoviesService service)
        {
            _service = service;
            
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var MoviesList= await _service.GetAllAsync(n=> n.Cinema);
            return View(MoviesList);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter( string searchString)
        {
            var MoviesList = await _service.GetAllAsync(n => n.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult= MoviesList.Where(n=> n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();
                return View("Index", filteredResult);
            }
            return View("Index", MoviesList);
        }


        //GET: Movies/Details/1
        [AllowAnonymous]
        public async Task <IActionResult> Details(int id) {

            var movieDetails = await _service.GetMovieByIdAsync(id);
            return View(movieDetails);
        }


        //GET

        public async Task<IActionResult> Create() {

            var movieDropdownData= await _service.GetNewMovieDropdownsValues();

            ViewBag.CinemaId = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
            ViewBag.ProducerId = new SelectList(movieDropdownData.Producers, "Id", "FullName");
            ViewBag.ActorIds = new SelectList(movieDropdownData.Actors, "Id", "FullName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var movieDropdownData = await _service.GetNewMovieDropdownsValues();
                ViewBag.CinemaId = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
                ViewBag.ProducerId = new SelectList(movieDropdownData.Producers, "Id", "FullName");
                ViewBag.ActorIds = new SelectList(movieDropdownData.Actors, "Id", "FullName");

                return View(movie);
            }
            await _service.AddNewMovieAsync(movie);
            return RedirectToAction("Index");
        }

        //GET
        //Movies/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails= await _service.GetMovieByIdAsync(id);

            if (movieDetails == null)
            {
                return View("NotFound");
            }

            var response = new NewMovieVM()
            {
                Id= movieDetails.Id,
                Name= movieDetails.Name,
                Description= movieDetails.Description,
                Price= movieDetails.Price,
                StartDate= movieDetails.StartDate,
                EndDate= movieDetails.EndDate,
                ImageURL= movieDetails.ImageURL,
                MovieCategory= movieDetails.MovieCategory,
                CinemaId= movieDetails.CinemaId,
                ProducerId= movieDetails.ProducerId,
                ActorIds= movieDetails.Movie_Actor.Select(n=> n.ActorId).ToList(),


            };

            var movieDropdownData = await _service.GetNewMovieDropdownsValues();

            ViewBag.CinemaId = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
            ViewBag.ProducerId = new SelectList(movieDropdownData.Producers, "Id", "FullName");
            ViewBag.ActorIds = new SelectList(movieDropdownData.Actors, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NewMovieVM movie)
        {
            if (id != movie.Id) return View("NotFound");
            if (!ModelState.IsValid)
            {
                var movieDropdownData = await _service.GetNewMovieDropdownsValues();
                ViewBag.CinemaId = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
                ViewBag.ProducerId = new SelectList(movieDropdownData.Producers, "Id", "FullName");
                ViewBag.ActorIds = new SelectList(movieDropdownData.Actors, "Id", "FullName");

                return View(movie);
            }
            await _service.UpdateNewMovieAsync(movie);
            return RedirectToAction("Index");
        }
    }
}
