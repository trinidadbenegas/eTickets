using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Producer: IEntityBase
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Profile Picture is required")]
        [Display(Name = "Profile Picture URL")]
        public string ProfilePictureURL { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Biography is required")]
        
        public string Bio { get; set; }

        public List<Movie>? Movies { get; set; }
    }
}
