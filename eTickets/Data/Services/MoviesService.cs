using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class MoviesService: EntityBaseRepository<Movie>, IMoviesService
    {

        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context): base(context)
        {
            _context = context;
            
        }

        public async Task AddNewMovieAsync(NewMovieVM data)
        {
            var newMovie = new Movie()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                CinemaId = data.CinemaId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId,


            };

           await _context.Movies.AddAsync(newMovie);
           await _context.SaveChangesAsync();

            //Add Movie Actors

            foreach( var actorId in data.ActorIds)
            {
                var newMovieActor = new Movie_Actor()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId,
                };

                await _context.Movies_Actors.AddAsync(newMovieActor);
            };

            await _context.SaveChangesAsync();

            
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var MovieDatails = await _context.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Movie_Actor).ThenInclude(a=> a.Actor)
                .FirstOrDefaultAsync(n=> n.Id == id);

            return MovieDatails;
             
            
        }

        public async Task<NewMovieDropdownVM> GetNewMovieDropdownsValues()
        {
            var response= new NewMovieDropdownVM();
            response.Actors= await _context.Actors.OrderBy(a=>a.FullName).ToListAsync();
            response.Cinemas= await _context.Cinemas.OrderBy(c=>c.Name).ToListAsync();
            response.Producers = await _context.Producers.OrderBy(p => p.FullName).ToListAsync();

            return response;
        }

        public async Task UpdateNewMovieAsync(NewMovieVM data)
        {
            var dbMovie= await _context.Movies.FirstOrDefaultAsync(n=> n.Id==data.Id);

            if (dbMovie != null)
            {
               
                    dbMovie.Name = data.Name;
                    dbMovie.Description = data.Description;
                    dbMovie.Price = data.Price;
                    dbMovie.ImageURL = data.ImageURL;
                    dbMovie.CinemaId = data.CinemaId;
                    dbMovie.StartDate = data.StartDate;
                    dbMovie.EndDate = data.EndDate;
                    dbMovie.MovieCategory = data.MovieCategory;
                    dbMovie.ProducerId = data.ProducerId;
                    
                    await _context.SaveChangesAsync();


            }


            //Remove existing actors

            var existingActorIds = _context.Movies_Actors.Where(n => n.MovieId == data.Id).ToList();
            _context.Movies_Actors.RemoveRange(existingActorIds);
            await _context.SaveChangesAsync();
            

            //Add Movie Actors

            foreach (var actorId in data.ActorIds)
            {
                var newMovieActor = new Movie_Actor()
                {
                    MovieId = data.Id,
                    ActorId = actorId,
                };

                await _context.Movies_Actors.AddAsync(newMovieActor);
            };

            await _context.SaveChangesAsync();
        }
    }
}
