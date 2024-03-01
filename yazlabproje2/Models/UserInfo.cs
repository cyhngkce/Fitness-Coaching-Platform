using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace yazlabproje2.Models
{
    public class UserInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Goal { get; set; }
        public string? Weight { get; set; }
        public string? Height { get; set; }
        public string? FatRate { get; set; }
        public string? BodyMassIndex { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
