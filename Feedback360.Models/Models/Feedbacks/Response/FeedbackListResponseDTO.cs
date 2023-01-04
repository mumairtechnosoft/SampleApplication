using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Models.Models.Feedbacks.Response
{
    public class FeedbackListResponseDTO
    {
        public long Id { get; set; }
        public long Category_id { get; set; }
        public long Severity_Id { get; set; }
        public long Status { get; set; }
        public string Comments { get; set; }
        public string Created_By { get; set; }
        public DateTime Modified_Date { get; set; }
        public string User_Name { get; set; }
    }
}
