using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace MemeryBank.Api.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LoginCustomMiddleware
    {
        private readonly RequestDelegate _next;

        public LoginCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            int statusCode = httpContext.Response.StatusCode;
            await httpContext.Response.WriteAsync("<h3>----Login custom Middleware----</h3>");

            //Read response body as stream
            StreamReader reader = new StreamReader(httpContext.Request.Body);
            string body = await reader.ReadToEndAsync();

            //Parse the request body from string into Dictionary
            Dictionary<string, StringValues>? queryDict = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);

            string? email = null, password = null;

            if (httpContext.Request.Path == "/" && httpContext.Request.Method == "POST")
            {

                if (queryDict.ContainsKey("email"))
                {
                    email = Convert.ToString(queryDict["email"][0]);
                }
                else
                {
                   // httpContext.Response.StatusCode = 400;
                    await httpContext.Response.WriteAsync("Email is a cumpulsory field\n");
                }

                if (queryDict.ContainsKey("password"))
                {
                    password = Convert.ToString(queryDict["password"][0]);
                }
                else
                {
                    if (statusCode == 200) statusCode = 400;
                    await httpContext.Response.WriteAsync("Password is a cumpulsory field\n");
                }

                string validEmail = "admin@example.com";
                string validPassword = "admin12345";
                bool isValidLogin;

                if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password))
                {
                    isValidLogin = (email == validEmail && password == validPassword);
                    if (isValidLogin)
                    {
                        await httpContext.Response.WriteAsync("Login successful\n");
                    }
                    else
                    {
                        httpContext.Response.StatusCode = 400;
                        await httpContext.Response.WriteAsync("Invalid credentials\n");
                    }
                }
            } else
            {
                await _next(httpContext);
            }

            await httpContext.Response.WriteAsync("<h3>----Login custom Middleware----</h3>");
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LoginCustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoginCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoginCustomMiddleware>();
        }
    }
}
