using DarkLoop.Azure.Functions.Authorize;
using Feedback360.DB.Entities;
using Feedback360.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OpenIddict.Validation.AspNetCore;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Feedback360.Functions
{
    public class AddFeedback
    {
        private readonly DbHelperContext _context;

        public AddFeedback(DbHelperContext context)
        {
            _context = context;
        }

        [FunctionAuthorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        [FunctionName("AddFeedback")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Feedback>(requestBody);
            data.Created_By = "Manager";
            data.Created_Date = DateTime.Now;
            data.Modified_By = "Manager";
            data.Modified_Date = DateTime.Now;
            data.Deleted = false;
            _context.Feedbacks.Add(data);
            await _context.SaveChangesAsync();
            return new OkObjectResult(GenericResponse<bool>.Success(true, "Added Successfully!"));
        }
    }
}
