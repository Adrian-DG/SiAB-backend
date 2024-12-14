using Microsoft.OpenApi.Models;

namespace SiAB.API.Services
{
	public static class SwaggerService
	{
		public static IServiceCollection AddSwaggerService(this IServiceCollection services)
		{
			return services.AddSwaggerGen(c => {
					c.SwaggerDoc("v1", new OpenApiInfo
					{
						Title = "SiAB API",
						Version = "v1"
					});
					c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
					{
						Name = "Authorization",
						Type = SecuritySchemeType.Http,
						Scheme = "Bearer",
						BearerFormat = "JWT",
						In = ParameterLocation.Header,
						Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
					});
					c.AddSecurityRequirement(new OpenApiSecurityRequirement {
					{
						new OpenApiSecurityScheme {
							Reference = new OpenApiReference {
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[] {}
					}
				});
			 });


		}
	}
}
