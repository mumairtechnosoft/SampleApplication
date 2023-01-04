using Feedback360.DB.General;
using System.ComponentModel.DataAnnotations;

namespace Feedback360.DB.Entities
{
    public class Role : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
