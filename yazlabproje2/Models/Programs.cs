using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace yazlabproje2.Models
{
    public class Programs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? TrainerId { get; set; }
        public string? Exercise { get; set; }
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        public int? Duration { get; set; }
    }
}
