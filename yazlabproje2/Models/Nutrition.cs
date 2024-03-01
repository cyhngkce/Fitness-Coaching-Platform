using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace yazlabproje2.Models
{
    public class Nutrition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? TrainerId { get; set; }
        public string? Goal { get; set; }
        public string? Meals { get; set; }
        public string? CalorieGoal { get; set; }

    }
}
