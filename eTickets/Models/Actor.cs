using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Actor: IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Profile Picture URL")]
        [Required]
        public string ProfilePictureURL { get; set; }
        [Display(Name = "Full Name")]
        [Required]
        public string FullName { get; set; }
        
        [Display(Name = "Biography")]
        [Required]
        public string Bio { get; set; }

        //Relationships 

        public List<Movie_Actor>? Movie_Actor { get; set; }

      
    }
}
