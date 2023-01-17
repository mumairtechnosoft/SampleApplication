using AzureFunctions.Extensions.Swashbuckle;
using AzureFunctions.Extensions.Swashbuckle.Settings;
using Feedback360.DB;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Validation.AspNetCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Reflection;

[assembly: FunctionsStartup(typeof(Feedback360.Functions.Startup))]

namespace Feedback360.Functions
{
    public partial class Startup : FunctionsStartup
    {

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
            }).AddCookie(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme).Services.AddOpenIddict().AddValidation(options =>
            {
                options.UseLocalServer();
            });

            builder.Services.AddDbContext<DbHelperContext>(
                options =>
                {
                    options.UseSqlServer("Server=TS-UMAIR-PC;Database=Feedback360;User Id=sa;password=Ts123456!;Trusted_Connection=false;MultipleActiveResultSets=true;");
                    options.UseOpenIddict();
                });

    //        builder.Services.AddOpenIddict()
    //.AddCore(options =>
    //{
    //    options.UseEntityFrameworkCore()
    //        .UseDbContext<ApplicationDbContext>();
    //})
    //.AddValidation(options =>
    //{
    //    // Import the configuration from the local OpenIddict server instance.
    //    options.UseLocalServer();

    //    // Register the ASP.NET Core host.
    //    options.UseAspNetCore();
    //});

            builder.AddAuthorization();
            builder.AddSwashBuckle(Assembly.GetExecutingAssembly(), opts =>
            {
                opts.AddCodeParameter = true;
                opts.Documents = new[] {
                    new SwaggerDocument {
                        Name = "v1",
                            Title = "Swagger document",
                            Description = "Integrate Swagger UI With Azure Functions",
                            Version = "v2"
                    }
                };
                opts.ConfigureSwaggerGen = x =>
                {
                    x.CustomOperationIds(apiDesc =>
                    {
                        return apiDesc.TryGetMethodInfo(out MethodInfo mInfo) ? mInfo.Name : default(Guid).ToString();
                    });
                };
            });
        }
    }
}
