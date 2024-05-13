using System.Text;

namespace BeepoApi
{
    public class AuthHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string username = Environment.GetEnvironmentVariable("API_Username");
        private readonly string password = Environment.GetEnvironmentVariable("API_Password");

        public AuthHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string authorizationHeader = context.Request.Headers["Authorization"];

            if (authorizationHeader != null && authorizationHeader.StartsWith("Basic"))
            {
                string encodedCredentials = authorizationHeader.Substring("Basic ".Length).Trim();
                string decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
                string[] credentials = decodedCredentials.Split(":");

                if (credentials.Length == 2 && credentials[0] == username && credentials[1] == password)
                {
                    await _next(context);
                    return;
                }
            }

            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized: invalid or missing credentials.");
        }
    }

    public static class AuthHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthHandlerMiddleware>();
        }
    }
}
