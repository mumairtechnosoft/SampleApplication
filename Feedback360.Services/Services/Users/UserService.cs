using AutoMapper;
using Feedback360.DB;
using Feedback360.Models;
using Feedback360.Models.Enums;
using Feedback360.Models.Models.Users.Response;
using Microsoft.EntityFrameworkCore;

namespace Feedback360.Services.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponse<List<EmployeeResponseDTO>>> GetAllEmployeesAsync()
        {
            var list = new List<EmployeeResponseDTO>();
            var listOfEmployees = await _context.Users.Where(x => x.Role == (long)Roles.Employee).ToListAsync();

            foreach (var item in listOfEmployees)
            {
                var emp = new EmployeeResponseDTO
                {
                    Id = item.Id,
                    Full_Name = item.First_Name + " " + item.Last_Name
                };
                list.Add(emp);
            }

            return GenericResponse<List<EmployeeResponseDTO>>.Success(list, "Data Successfully Retreived!");
        }

        public async Task<GenericResponse<EmployeeResponseDTO>> GetEmployeeByUsernameAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (user == null)
            {
                return GenericResponse<EmployeeResponseDTO>.Failure("Something went wrong!", ApiStatusCode.RecordNotFound);
            } else
            {
                var obj = new EmployeeResponseDTO
                {
                    Id = user.Id,
                    Full_Name = $"{user.First_Name} {user.Last_Name}"
                };
                return GenericResponse<EmployeeResponseDTO>.Success(obj, "Data Successfully Retreived!");
            }
        }
    }
}
