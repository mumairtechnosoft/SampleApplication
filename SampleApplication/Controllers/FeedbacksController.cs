using Feedback360.Models.Models.Feedbacks.Request;
using Feedback360.Services.Services.Feedbacks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OpenIddict.Server.AspNetCore;
using OpenIddict.Validation.AspNetCore;
using SampleApplication.Models;
using System.Text.Json;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using Formatting = Newtonsoft.Json.Formatting;
using System.Net.Http.Headers;
using Feedback360.Models;

namespace SampleApplication.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbacksService _feedbacksService;
        private readonly ILogger<FeedbacksController> _logger;
        private AppSettings AppSettings { get; set; }

        public FeedbacksController(IFeedbacksService feedbacksService, ILogger<FeedbacksController> logger, IOptions<AppSettings> settings)
        {
            _feedbacksService = feedbacksService;
            _logger = logger;
            AppSettings = settings.Value;
        }

        [HttpPost("addFeedback")]
        public async Task<IActionResult> AddFeedbackAsync(FeedbackAddRequestDTO requestDTO)
        {
            _logger.LogInformation("Adding Feedback...");

            var Url = AppSettings.AddFeedbackURL;

            dynamic content = requestDTO;

            CancellationToken cancellationToken = new CancellationToken();

            var json = new GenericResponse<bool>();

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, Url))
            using (var httpContent = CreateHttpContent(content))
            {
                request.Content = httpContent;

                using (var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false))
                {

                    var resualtList = response.Content.ReadAsStringAsync().Result;
                    json = JsonConvert.DeserializeObject<GenericResponse<bool>>(resualtList);
                }
            }

            return Ok(json);
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

        public static void SerializeJsonIntoStream(object value, Stream stream)
        {
            using (var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            using (var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None })
            {
                var js = new JsonSerializer();
                js.Serialize(jtw, value);
                jtw.Flush();
            }
        }

        private static HttpContent CreateHttpContent(object content)
        {
            HttpContent httpContent = null;

            if (content != null)
            {
                var ms = new MemoryStream();
                SerializeJsonIntoStream(content, ms);
                ms.Seek(0, SeekOrigin.Begin);
                httpContent = new StreamContent(ms);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            return httpContent;
        }
    }
}
