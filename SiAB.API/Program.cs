using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SiAB.API.Constants;
using SiAB.API.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(
		builder =>
		{
			builder
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader();
		});
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerService();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDatabaseServices(builder.Configuration, builder.Environment);

builder.Services.AddApplicationServices();

// Authentication & Authorization 

builder.Services.AddAuthentication(options => {
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
	options.RequireHttpsMetadata = true;
	options.SaveToken = true;
	options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = false,
        ValidIssuer = builder.Configuration[JwtBearerConstants.JWT_ISSUER],
        ValidAudience = builder.Configuration[JwtBearerConstants.Jwt_Audience],
		TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration[JwtBearerConstants.JWT_KEY])),
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration[JwtBearerConstants.JWT_KEY])),
		ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

// builder.Services.AddAuthorization(options =>
// {
//   options.AddPolicy("Admin", policy => policy.RequireRole("admin"));
// });


// Configuraci�n de Autenticaci�n y Autorizaci�n

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
}
else
{
	app.UseSwagger();
	app.UseSwaggerUI(c => {
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "SiAB API v1");
		c.RoutePrefix = string.Empty;
	});
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
