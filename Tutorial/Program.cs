var builder = WebApplication.CreateBuilder(args); // this line setsup and create the kestral server 

var app = builder.Build(); //generates the instance of the web application
//this is a middleware pipeline component
string name = "Abdullah";
app.MapGet("/", () => $"Hello World! this is {name} learning ASP.Net");
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync($"The Method is {context.Request.Method}");
    await context.Response.WriteAsync($"The Path is {context.Request.Path}");
    await context.Response.WriteAsync($" Headers ");
    foreach (var key in context.Request.Headers)
    {
        await context.Response.WriteAsync($"{key}: {key.Value}\n");
    }
});

app.Run();// runs the application and starts listening for incoming HTTP requests.(starts the kestral server)
