using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yazlabproje2.Models
{


    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        
        public string? Name { get; set; }
        
        public string? Surname { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        public string? ProfilePicture { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}