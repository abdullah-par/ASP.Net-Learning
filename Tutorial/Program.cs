var builder = WebApplication.CreateBuilder(args); // this line setsup and create the kestral server 

var app = builder.Build(); //generates the instance of the web application
//this is a middleware pipeline component
string name = "Abdullah";
app.MapGet("/", () => $"Hello World! this is {name} learning ASP.Net");
app.Run(async (HttpContext context) =>
{
    if (context.Request.Method == "GET") {
        if(context.Request.Path.StartsWithSegments("/"))
        {
            await context.Response.WriteAsync($"The Method is {context.Request.Method}");
            await context.Response.WriteAsync($"The Path is {context.Request.Path}");
            await context.Response.WriteAsync($" Headers ");
            foreach (var key in context.Request.Headers)
            {
                await context.Response.WriteAsync($"{key}: {key.Value}\n");
            }

        }
        else if(context.Request.Path.StartsWithSegments("/Students"))
        {
            var students =  StudentsRepository.GetStudents();
            foreach(var student in students)
            {
                await context.Response.WriteAsync($"{student.Name} enrolled in {student.Branch}\r\n");
            }
        }
        
    }
    
});

app.Run();// runs the application and starts listening for incoming HTTP requests.(starts the kestral server)

static class StudentsRepository
{
    private static List<Student> students = new List<Student>
    {
        new Student (1, "Abdullah", "AIML"),
        new Student (2, "Abdul", "AIML"),
        new Student (2, "Rabiul", "AIML")
    };
    public static List<Student> GetStudents() => students;
}
class Student
{
    public int Id { get; set; }
    public String Name { get; set; }
    public String Branch { get; set; }
    public Student(int id, string name, string branch)
    {
        Id = id; 
        Name = name;
        Branch = branch;
    }
}