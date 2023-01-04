using Feedback360.Models.Enums;

namespace Feedback360.Models
{
    public class GenericResponse<T>
    {
        public string message { get; set; }
        public bool status { get; set; }
        public int errorCode { get; set; }
        public T payload { get; set; }

        public static GenericResponse<T> Success(T payload, string message)
        {
            return new GenericResponse<T>
            {
                errorCode = 0,
                status = true,
                message = message,
                payload = payload
            };
        }
        public static GenericResponse<T> Success(string message)
        {
            return new GenericResponse<T>
            {
                errorCode = 0,
                status = true,
                message = message,
            };
        }
        public static GenericResponse<T> Failure(string message, ApiStatusCode apiStatusCode)
        {
            return new GenericResponse<T>
            {
                errorCode = (int)apiStatusCode,
                status = false,
                message = message,
            };
        }
        public static GenericResponse<T> FailureCustomized(string message, string DynamicMessage, ApiStatusCode apiStatusCode)
        {
            return new GenericResponse<T>
            {
                errorCode = (int)apiStatusCode,
                status = false,
                message = message + DynamicMessage,
            };
        }

        public static GenericResponse<T> Failure(T payload, string message, ApiStatusCode apiStatusCode)
        {
            return new GenericResponse<T>
            {
                errorCode = (int)apiStatusCode,
                status = false,
                message = message,
                payload = payload
            };
        }
    }
}
