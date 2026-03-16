namespace APIApplication.PipelineObjects;

public class ExceptionMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<ExceptionMiddleware> _logger;

	public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context); // Tenta seguir o fluxo normal
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Ocurrió un error no controlado."); // Log técnico interno
			await HandleExceptionAsync(context);
		}
	}

	private static Task HandleExceptionAsync(HttpContext context)
	{
		context.Response.ContentType = "application/json";
		context.Response.StatusCode = 500;

		var response = new
		{
			status = 500,
			error_code = "INTERNAL_SERVER_ERROR",
			message = "Lo sentimos, algo salió mal en nuestros servidores. Por favor, inténtalo de nuevo más tarde."
		};

		return context.Response.WriteAsJsonAsync(response);
	}
}