using Feedback360.Services.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SampleApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("listOfEmployees")]
        public async Task<IActionResult> GetListOfEmployees()
        {
            return Ok(await _userService.GetAllEmployeesAsync());
        }

        [HttpGet("getEmployee")]
        public async Task<IActionResult> GetEmployeeByUsernameAsync(string username)
        {
            return Ok(await _userService.GetEmployeeByUsernameAsync(username));
        }
    }
}
