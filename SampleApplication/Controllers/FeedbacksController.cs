using Feedback360.Models.Models.Feedbacks.Request;
using Feedback360.Services.Services.Feedbacks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Server.AspNetCore;

namespace SampleApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbacksService _feedbacksService;
        private readonly ILogger<FeedbacksController> _logger;
        public FeedbacksController(IFeedbacksService feedbacksService, ILogger<FeedbacksController> logger)
        {
            _feedbacksService = feedbacksService;
            _logger = logger;
        }

        [HttpPost("addFeedback")]
        public async Task<IActionResult> AddFeedbackAsync(FeedbackAddRequestDTO requestDTO)
        {
            _logger.LogInformation("Adding Feedback...");
            return Ok(await _feedbacksService.AddFeedbackAsync(requestDTO));
        }

        [HttpGet("getAllByEmployeeId")]
        public async Task<IActionResult> GetListOfFeedbacksByEmployeeIdAsync(long employeeId)
        {
            _logger.LogInformation("Getting list of feedbacks by employee id...");
            return Ok(await _feedbacksService.GetAllFeedbacksByEmployeeIDAsync(employeeId));
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetListOfFeedbacksAsync()
        {
            _logger.LogInformation("Getting list of feedbacks...");
            return Ok(await _feedbacksService.GetAllFeedbacksAsync());
        }
    }
}
