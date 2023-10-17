using eTickets.Models;

namespace eTickets.Data.ViewModels
{
    public class NewMovieDropdownVM
    {
        public NewMovieDropdownVM()
        {
            Cinemas = new List<Cinema>();
            Producers = new List<Producer>();
            Actors= new List<Actor>();

        }
        public List<Actor> Actors { get; set; }
        public List<Producer> Producers { get; set; }

        public List<Cinema> Cinemas { get; set; }
    }
}
