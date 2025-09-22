var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "CRM", Version = "v1" });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "CRM V1");
	}); // This requires 'using Swashbuckle.AspNetCore.SwaggerUI;'
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();