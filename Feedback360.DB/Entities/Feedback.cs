using Feedback360.DB.General;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Feedback360.DB.Entities
{
    [Table("Feedbacks")]
    public class Feedback : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long Employee_Id { get; set; }

        [Required]
        public long Category_Id { get; set; }

        [Required]
        public long Severity_Id { get; set; }

        public string? Visibility { get; set; }

        public string? Feedback_Target { get; set; }

        [Required]
        [MaxLength(500)]
        public string Comments { get; set; }

        public long Status { get; set; }
    }
}
