using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace yazlabproje2.Models
{
    public class Trainer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }

        public string? Profession { get; set; }
        public string? Experience { get; set; }

    }
}
