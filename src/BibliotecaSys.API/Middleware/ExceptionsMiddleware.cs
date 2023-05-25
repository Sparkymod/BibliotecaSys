using System.ComponentModel.DataAnnotations;
using FluentValidation;
using FluentValidation.Results;
using System.Net;
using System.Text.Json;

namespace BibliotecaSys.API.Middleware;

public static class ExceptionsMiddleware
{
    /// <summary>
    ///     Extension method to add the ExceptionCatcherMiddleware to the IApplicationBuilder's middleware pipeline.
    /// </summary>
    /// <param name="app">The IApplicationBuilder instance for which to add the ExceptionCatcherMiddleware.</param>
    /// <returns>The updated IApplicationBuilder instance with the ExceptionCatcherMiddleware added to the pipeline.</returns>
    public static IApplicationBuilder UseExceptionCatcherMiddleware(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            try
            {
                await next(context);
            }
            catch (FluentValidation.ValidationException exception)
            {
                var errors = ConvertValidationErrorsToDictionary(exception.Errors);
                var jsonResponse = JsonSerializer.Serialize(errors);

                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(jsonResponse);
            }
            catch (Exception ex)
            {
                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync($"{{ \"unexpectedError\": {ex.Message } }}");
            }
        });

        return app;
    }

    private static IDictionary<string, string> ConvertValidationErrorsToDictionary(IEnumerable<ValidationFailure> errors)
    {
        return errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage);
    }
}