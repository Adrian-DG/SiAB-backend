using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SiAB.API.Constants;
using SiAB.API.Handlers;
using SiAB.API.Middlewares;
using SiAB.API.Services;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowSpecificOrigin",
		builder =>
		{
			builder
				//.WithOrigins("http://localhost:4200", "https://192.168.4.34")	
				.AllowAnyOrigin()			
				.AllowAnyMethod()
				.AllowAnyHeader()
				.WithExposedHeaders("content-disposition")
				.SetPreflightMaxAge(TimeSpan.FromMinutes(10));
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
	options.RequireHttpsMetadata = false;
	options.SaveToken = true;
	options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = false,
        ValidIssuer = builder.Configuration[JwtBearerConstants.JWT_ISSUER],
        ValidAudience = builder.Configuration[JwtBearerConstants.Jwt_Audience],
		TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration[JwtBearerConstants.JWT_KEY])),
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration[JwtBearerConstants.JWT_KEY])),
		ClockSkew = TimeSpan.Zero,
		RequireExpirationTime = true,
	};
});

builder.Services.AddAuthorization();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Configuraci�n de Autenticaci�n y Autorizaci�n

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
}
else
{
	app.UseSwagger();
	app.UseSwaggerUI(c => {
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
		c.RoutePrefix = string.Empty;
	});
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ApiResponseMiddleware>();
app.UseExceptionHandler();

app.MapControllers();

app.Run();
