using Microsoft.AspNetCore.Mvc;
using SiAB.API.Attributes;
using SiAB.Core.Models;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace SiAB.API.Middlewares
{
	public class ApiResponseMiddleware : IMiddleware
	{	
		
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			var endpoint = context.GetEndpoint();
			if (endpoint != null)
			{
				var endpointMetadata = endpoint.Metadata.OfType<AvoidCustomResponse>().Any();

				if (endpointMetadata)
				{
					await next(context);
					return;
				}
			}			

			// Capture the request body
			context.Request.EnableBuffering();
			var requestBodyStream = new MemoryStream();
			await context.Request.Body.CopyToAsync(requestBodyStream);
			requestBodyStream.Seek(0, SeekOrigin.Begin);
			var requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();
			requestBodyStream.Seek(0, SeekOrigin.Begin);
			context.Request.Body = requestBodyStream;

			// Call the next middleware in the pipeline
			var originalBodyStream = context.Response.Body;
			using var responseBodyStream = new MemoryStream();
			context.Response.Body = responseBodyStream;

			await next(context);

			// Customize the response
			context.Response.Body.Seek(0, SeekOrigin.Begin);
			var responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
			context.Response.Body.Seek(0, SeekOrigin.Begin);

			object? responseData = null;
			if (!string.IsNullOrEmpty(responseBodyText))
			{
				responseData = JsonSerializer.Deserialize<object>(responseBodyText);
			}

			string message;
			if (context.Response.StatusCode == StatusCodes.Status200OK)
			{
				message = "La solicitud se ejecuto de manera existosa";
			}
			else
			{
				var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(responseBodyText);
				message = problemDetails?.Detail ?? "Ocurrio un error";
			}

			var customResponse = new ApiResponse
			{
				Title = context.Response.StatusCode == StatusCodes.Status200OK ? "Ok" : "Error",
				Message = message,
				Status = context.Response.StatusCode == StatusCodes.Status200OK,
				Code = context.Response.StatusCode,
				Data = context.Response.StatusCode == StatusCodes.Status200OK ? responseData : null
			};

			var customResponseJson = JsonSerializer.Serialize(customResponse, options: new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
			context.Response.ContentType = "application/json";

			// Write the custom response to the original response body stream
			using var customResponseStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(customResponseJson));
			await customResponseStream.CopyToAsync(originalBodyStream);
		}
	}	
}
