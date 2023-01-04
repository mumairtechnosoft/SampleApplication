using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Models.Models.Feedbacks.Request
{
    public class FeedbackAddRequestDTO
    {
        public int Id { get; set; }

        public long Employee_Id { get; set; }

        public long Category_Id { get; set; }

        public long Severity_Id { get; set; }

        public string? Visibility { get; set; }

        public string? Feedback_Target { get; set; }

        public string Comments { get; set; }

        public long Status { get; set; }
    }
}
