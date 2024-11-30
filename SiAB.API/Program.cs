using Microsoft.AspNetCore.Identity;
using SiAB.API.Services;
using SiAB.Core.Entities.Auth;
using SiAB.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddIdentity<Usuario, Role>(opt => {
	opt.SignIn.RequireConfirmedAccount = false;
	opt.Password.RequireNonAlphanumeric = false;
	opt.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddDatabaseServices(builder.Configuration, builder.Environment);

builder.Services.AddApplicationServices();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(typeof(Program));

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
app.MapControllers();

app.Run();
