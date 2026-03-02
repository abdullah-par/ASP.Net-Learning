var builder = WebApplication.CreateBuilder(args); // this line setsup and create the kestral server 

var app = builder.Build(); //generates the instance of the web application
//this is a middleware pipeline component
string name = "Abdullah";
app.MapGet("/", () => $"Hello World! this is {name} learning ASP.Net");

app.Run();// runs the application and starts listening for incoming HTTP requests.(starts the kestral server)
