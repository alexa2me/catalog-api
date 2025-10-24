
using System.Net;

using APICatalog.Models;

using Microsoft.AspNetCore.Diagnostics;

namespace APICatalog.Extensions;

public static class ApiExceptionMiddlewareExtensions
{
	public static void ConfigureExceptionHandler(this IApplicationBuilder app)
	{
		app.UseExceptionHandler(appError =>
		{
			appError.Run(async context =>
			{
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				context.Response.ContentType = "application/json";

				var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
				if (contextFeature != null)
				{
					var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
					await context.Response.WriteAsync(new ErrorDetails()
					{
						StatusCode = context.Response.StatusCode,
						Message = contextFeature.Error.Message,
						Trace = env == "Development" ? contextFeature.Error.StackTrace : null
					}.ToString());
				}
			});
		});
	}
}