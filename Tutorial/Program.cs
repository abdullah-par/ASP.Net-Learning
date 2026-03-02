using System.Text.Json;

var builder = WebApplication.CreateBuilder(args); // this line setsup and create the kestral server 

var app = builder.Build(); //generates the instance of the web application
//this is a middleware pipeline component
string name = "Abdullah";
app.MapGet("/", () => $"Hello World! this is {name} learning ASP.Net");
app.Run(async (HttpContext context) =>
{
    if (context.Request.Method == "GET") {
        if(context.Request.Path =="/")
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
                await context.Response.WriteAsync($"{student.Name} enrolled in {student.Branch} \r\n");
            }
        }
        
    }
    else if (context.Request.Method == "POST") {
        if (context.Request.Path.StartsWithSegments("/Students"))
        {
            using var reader = new StreamReader(context.Request.Body);
            // body variable containing JSON data sent.
            var body = await reader.ReadToEndAsync();
            // Deserialize converts a JSON string into an object (here class(Student))
            //JsonSerializer : handles JSON operations
            //<Student> : is generic type parameter
            var student  = JsonSerializer.Deserialize<Student>(body);
            StudentsRepository.AddStudent(student);
        }
    }
    
});

app.Run();// runs the application and starts listening for incoming HTTP requests.(starts the kestral server)

//This is Respository Pattern, which is a design pattern that provides a way to manage data access and storage in an application. It abstracts the data layer and provides a simple interface for accessing and manipulating data. In this case, the StudentsRepository class serves as a repository for managing student data. It provides a method GetStudents() that returns a list of students, allowing other parts of the application to interact with the student data without needing to know the details of how it is stored or accessed.
static class StudentsRepository
{
    //static because we are using it as a simple data storage. (Repository Pattern)
    private static List<Student> students = new List<Student>
    //these curly braces are used to initialize the list with some predefined student objects. This is called collection initializer syntax in C#. It allows us to create and populate the list in a concise way. Each new Student object is created with an Id, Name, and Branch, and added to the students list. They are called Collection Initializer Syntax
    {
        new Student (1, "Abdullah", "AIML"),
        new Student (2, "Abdul", "AIML"),
        new Student (3, "Rabiul", "AIML")
    };
    // => is called an expression-bodied member. It is a concise way to define a method that consists of a single expression. In this case, the GetStudents method returns the students list directly without needing a block of code. 
    public static List<Student> GetStudents() => students;
    public static void AddStudent(Student student) { students.Add(student); }
}
class Student
{
    public int Id { get; set; } //auto property for Id
    public String Name { get; set; }
    public String Branch { get; set; }
    public Student(int id, string name, string branch)
    {
        Id = id; 
        Name = name;
        Branch = branch;
    }
}