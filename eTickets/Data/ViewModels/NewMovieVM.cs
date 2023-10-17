using eTickets.Data;
using eTickets.Data.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class NewMovieVM
    {
        public int Id { get; set; }
        [Display(Name="Movie Name")]
        [Required(ErrorMessage ="Name is required")] 
        public string Name { get; set; }

        [Display(Name = "Movie Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Price in $")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Name="Movie Poster ")]
        [Required(ErrorMessage ="Image URL is required")]
        public string ImageURL { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }

        
        [Display(Name="Select a Category")]
        [Required(ErrorMessage = "Category is required")]
        public MovieCategory  MovieCategory { get; set; }

        [Display(Name = "Select one o more actors")]
        [Required(ErrorMessage = "Actors are required")]
        public List<int> ActorIds{ get; set; }

        [Display(Name = "Select a cinema")]
        [Required(ErrorMessage = "Cinema is required")]
        public int CinemaId { get; set; }

        [Display(Name = "Select a producer")]
        [Required(ErrorMessage = "Producer is required")]
        public int ProducerId { get; set; }
        


    }
}
