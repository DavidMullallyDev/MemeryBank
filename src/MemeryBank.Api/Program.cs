using MemeryBank.Api.Constraints;
using MemeryBank.Api.Middleware;
using MemeryBank.Api.ModelBinders;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Services;
using System.Dynamic;

var builder = WebApplication.CreateBuilder(args);
// Modelbinders will be executed in the order they are in thats why i inserted this at 0
builder.Services.AddControllersWithViews(options =>
{
   // options.ModelBinderProviders.Insert(0, new CustomPersonModelBinderProvider()); //if you want to use custom model binders
}).AddXmlSerializerFormatters();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
// if you want to be able to read xml from the request body
//// If you want to use a folder other that wwwroot for static files 
//// wwwroot folder must still exist though
// var builder = WebApplication.CreateBuilder(WebApplIcationOptions()
//{ 
// webRootPath = "myRoot";
//})

builder.Services.AddTransient<MyCustomMiddleware>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("months", typeof(MonthsCustomConstraint));
});
builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();
app.MapRazorPages();
app.MapBlazorHub();

//(introduced in asp.net core 6) Routing is automatically enabled
//no need for app.UseRouting anymore
//endpoints are defined directly on the "app" object

//// order of middleware recommended by Microsoft
//app.UseExceptionHandler("/Error");
//app.UseHsts();
//app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseRouting();
//app.UseCors();
//app.UseAuthorization();
//app.UseAuthorization();
//app.UseSession();
//app.MapControllers();

//// order of endpoint selection when more thn one route match exists
// 1)- The Endpoint with more segments   e.g a/b/c/d > a/b/c
// 2)- endpoint with more literals  e.g a/b > a/{parameter}
// 3)- with constrainst vs without e.g a/{b:int} > a/{b}
// 4)- catch all parameters (**) e.g a/{b} > a/**

//add your custom middleware

app.UseStaticFiles(); //works with wwwroot by default if theres a webrootpath specified in the builder

//`//if you want to have more than one folder for static files then call another app.UseStaticFiles as follows
//app.UseStaticFiles(new StaticFileOptions()
//{
// FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, @"\myWebRoot"))
//}); works with myWebRoot

app.Map("memes/{fileName}.{fileExt}", async (context) =>
{
    string? fileName = $"{context.Request.RouteValues["fileName"]?.ToString()}.{context.Request.RouteValues["fileExt"]?.ToString()}";
    await context.Response.WriteAsync($"{fileName} retrieved successfully");
});

//Parameter with default value
//Parameter(s) with constraints
//alpha - only accepts srtings containing letters aA-zZ

app.Map("users/profile/{firstname:alpha:minlength(4)=john}&{middlename:maxLength(7)}&{lastName:length(4,7)}", async (context) =>
{
    string? username = $"{context.Request.RouteValues["firstname"]?.ToString()} {context.Request.RouteValues["middlename"]?.ToString()} {context.Request.RouteValues["lastname"]?.ToString()}";
    await context.Response.WriteAsync($"Welcome {username}");
});

app.Map("users/profile/{age:regex(^[0-9]{{2}}$)}", async (context) =>
{
    int? userAge = Convert.ToInt32(context.Request.RouteValues["age"]);
    await context.Response.WriteAsync($"User is {userAge} yrs old.");
});

app.Map("users/profile/{phonenumber:regex(^\\d{{4}}-\\d{{7}}$)}", async (context) =>
{
    string? phoneNumber = Convert.ToString(context.Request.RouteValues["phonenumber"]);
    await context.Response.WriteAsync($"phone number: {phoneNumber}");
});

//optional parameter

//int constraints - max, min, range
app.Map("products/details/{id:int:range(1,100)?}", async (context) =>
{
    int? productId = Convert.ToInt32(context.Request.RouteValues["id"]);
    if(productId == null)
    {
        await context.Response.WriteAsync($"Please supply a product Id");
    } else
    {
        await context.Response.WriteAsync($"Showing product: {productId}");
    }
});

app.Map("daily-digest-report/{reportdate:datetime}", async (context) =>
{
    DateTime reportDate = Convert.ToDateTime(context.Request.RouteValues["reportdate"]);
    await context.Response.WriteAsync($"report date: {reportDate.ToShortDateString()}");
});

app.Map("sales-report/{month:months}", async(context)=>
{
    string? month = Convert.ToString(context.Request.RouteValues["month"]);
    await context.Response.WriteAsync($"Report from {month}");
});

app.Map("cities/{cityid:guid}", async (context) =>
{
    Guid cityId = Guid.Parse(Convert.ToString(context.Request.RouteValues["cityid"])!);
    await context.Response.WriteAsync($"Welcome to {cityId}");
});
    

//fallback for any ozjet requests
app.MapFallback(async (context) =>
{
    await context.Response.WriteAsync($"{context.Request.Path} was not found");
});

//app.Use(async (HttpContext context, RequestDelegate next) =>
//{
//    context.Response.StatusCode = 400;
//    context.Response.Headers["myKey"] = "1234";
//    context.Response.Headers.ContentType = "text/html";
//    await context.Response.WriteAsync("<h1>middleware1<h1>");
//    await next(context);
//});

////app.UseLoginCustomMiddleware();

//app.UseMyCustomMiddleware();

//app.UseHelloCustomMiddleware();

//app.UseWhen(
//    context => context.Request.Query.ContainsKey("username"),
//    app =>
//    {
//        app.Use(async (context, next) =>
//        {
//            await context.Response.WriteAsync("Hello from the Middleware If branch");
//            await next();
//        });
//    });

//app.MapGet("/", () => "Hello World!");

//app.Run(async (HttpContext context) =>
//{
//    //context.Response.StatusCode = 400;
//    //context.Response.Headers["myKey"] = "1234";
//    string path = context.Request.Path;
//    string method = context.Request.Method;
//    //context.Response.Headers.ContentType = "text/html";
//    await context.Response.WriteAsync("<h1>Hello<h1>");
//    await context.Response.WriteAsync("<h2> World!<h2>");
//    await context.Response.WriteAsync($"<h5>{path}</h5>");
//    await context.Response.WriteAsync($"<h5>{method}</h5>");

//    //stores all the query keys in a dictionary allowing them to be iterated through
//    dynamic queryData = new ExpandoObject();
//    var dict = (IDictionary<string, object>)queryData;

//    foreach (var key in context.Request.Query.Keys)
//    {
//        dict[key] = context.Request.Query[key].ToString();
//    }

//    foreach (KeyValuePair<string, object> kvp in dict)
//    {
//        await context.Response.WriteAsync($"<h5>{kvp.Key}={kvp.Value}");
//    }

//    if (context.Request.Headers.ContainsKey("User-Agent"))
//    {
//        string? userAgent = context.Request.Headers.UserAgent;
//        await context.Response.WriteAsync($"<h5>{userAgent}</h5>");
//    }

//    StreamReader reader = new(context.Request.Body);
//    string body = await reader.ReadToEndAsync();

//    // StringValues as one key can have multiple values eg httüs://localhost.435665/?id=1&age=20&age=30
//    Dictionary<String, StringValues>? dict2 = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);
//    if (dict2 != null && dict2.TryGetValue("firstName", out StringValues value))
//    {
//        string? firstName = dict2["firstName"][0];
//        await context.Response.WriteAsync($"<h5>{firstName}</h5>");
//    }
//    await context.Response.WriteAsync("Hello");
//});
app.Run();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

//app.UseHttpsRedirection();

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast =  Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast");

//app.Run();

//record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}
