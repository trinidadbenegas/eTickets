using eTickets.Data;
using eTickets.Data.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Movie: IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        [Display(Name="Image")]
        public string ImageURL { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Category")]
        public MovieCategory  MovieCategory { get; set; }

        //Relationships

        //
        public List<Movie_Actor> Movie_Actor{ get; set; }

        public int CinemaId { get; set; }
        [ForeignKey("CinemaId")]
        public Cinema Cinema { get; set; }

        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]

        public Producer Producer { get; set; }


    }
}
