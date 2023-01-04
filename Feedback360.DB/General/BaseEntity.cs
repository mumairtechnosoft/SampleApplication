using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.DB.General
{
    public class BaseEntity
    {
        public string Created_By { get; set; }

        public DateTime Created_Date { get; set; }

        public string Modified_By { get; set; }

        public DateTime Modified_Date { get; set; }

        public bool Deleted { get; set; }
    }
}
