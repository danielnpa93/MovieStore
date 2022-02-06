using TinyMovieShared.API.Exceptions;
using TinyMovieShared.API.Results;

namespace TinyMovieShared.API.Config
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(DomainException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new ResultViewModel() { Message = ex.Message, Errors = ex.Errors });
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new ResultViewModel() { Message = ex.Message});
            }
        }
    }
}
