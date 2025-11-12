using EngineApplication.Domain.Interfaces;
using InfrastructureApplication.Persistence;
using InfrastructureApplication.Persistence.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddScoped<IActivityRepository, ActivityRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "APIApplication", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIApplication v1");
	});
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();