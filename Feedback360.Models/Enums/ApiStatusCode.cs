using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Models.Enums
{
    public enum ApiStatusCode
    {
        InternalServerError = 500,
        Success = 0,
        RecordFound = 1,
        RecordNotFound = 2,
        SomethingWentWrong = 3
    }
}
