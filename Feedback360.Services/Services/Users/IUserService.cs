using Feedback360.Models;
using Feedback360.Models.Models.Users.Response;

namespace Feedback360.Services.Services.Users
{
    public interface IUserService
    {
        Task<GenericResponse<List<EmployeeResponseDTO>>> GetAllEmployeesAsync();
        Task<GenericResponse<EmployeeResponseDTO>> GetEmployeeByUsernameAsync(string username);
    }
}
