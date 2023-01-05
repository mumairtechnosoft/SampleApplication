using Feedback360.Services.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace SampleApplication.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
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
