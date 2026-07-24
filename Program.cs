
using Microsoft.EntityFrameworkCore;
using TmsApi.Data;
using Microsoft.AspNetCore.Authentication;
using Scalar.AspNetCore;
using TmsApi.Options;
using TmsApi.Entities;
using TmsApi.Services;

var builder = WebApplication.CreateBuilder(args);


// =======================
// Services
// =======================

builder.Services.AddControllers();


// OpenAPI + Scalar support
builder.Services.AddOpenApi();


// Standard error responses
builder.Services.AddProblemDetails();

builder.Services.AddDbContext<TmsDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("TmsDatabase"))
.LogTo(Console.WriteLine, LogLevel.Information) // Log SQL to output window
.EnableSensitiveDataLogging()); // Show parameters in querylogs (dev only)
// =======================
// Authentication
// =======================

builder.Services.AddAuthentication()
    .AddScheme<AuthenticationSchemeOptions, TrainingAuthHandler>(
        "Training",
        null
    );


// =======================
// Configuration Validation
// =======================

builder.Services.AddOptions<PaymentOptions>()
    .BindConfiguration("Payments")
    .ValidateDataAnnotations()
    .ValidateOnStart();


// =======================
// Authorization
// =======================

builder.Services.AddAuthorization();


// =======================
// Dependency Injection
// =======================

builder.Services.AddSingleton<EnrollmentWorker>();

builder.Services.AddSingleton<IEnrollmentService, EnrollmentService>();


// Validate dependency injection
builder.Host.UseDefaultServiceProvider(options =>
{
    options.ValidateScopes = true;
    options.ValidateOnBuild = true;
});
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<NPlusOneDemo>();
builder.Services.AddScoped<SoftDeleteDemo>();
builder.Services.AddScoped<BulkArchiveDemo>();
builder.Services.AddScoped<ICourseService, CourseService>();
<<<<<<< HEAD
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();

=======
>>>>>>> 07650c5 (m6)

var app = builder.Build();
Console.WriteLine("===== MY PROGRAM.CS IS RUNNING =====");
Console.WriteLine($"Environment: {app.Environment.EnvironmentName}");
Console.WriteLine($"Is Development: {app.Environment.IsDevelopment()}");

// =======================
// Middleware Pipeline
// =======================


// Global exception handling
app.UseExceptionHandler();


// Convert empty errors to ProblemDetails
app.UseStatusCodePages();


// Request logging
app.UseMiddleware<RequestLoggingMiddleware>();


app.UseHttpsRedirection();

app.UseRouting();


// Security middleware
app.UseAuthentication();

app.UseAuthorization();


// =======================
// Minimal API Endpoints
// =======================


// Protected assessment endpoint
app.MapGet("/api/assessments/results", () =>
{
    return Results.Ok(new
    {
        courseCode = "CS-101",
        studentId = "S-001",
        letterGrade = "A"
    });
})
.RequireAuthorization();


// Worker test endpoint
app.MapGet("/api/enrollments/worker-smoke",
    (EnrollmentWorker worker) =>
{
    worker.ProcessBatch();

    return Results.Ok("processed");
});


// =======================
// Controllers
// =======================

app.MapControllers();


// =======================
// Error Testing Endpoint
// =======================

app.MapGet("/api/error", () =>
{
    throw new TmsDatabaseException(
        "Simulated database failure for ProblemDetails testing");
});


// =======================
// Development Only Tools
// =======================

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();
}
<<<<<<< HEAD

// Seed test data at startup
/* using (var scope = app.Services.CreateScope())
=======
/* 
// Seed test data at startup
using (var scope = app.Services.CreateScope())
>>>>>>> 07650c5 (m6)
{
    var context = scope.ServiceProvider.GetRequiredService<TmsDbContext>();

    context.Database.Migrate(); // Applies any pending migrations; keeps migration history intact

    if (!context.Students.Any())
    {
        var students = new List<Student>
        {
            new() { RegistrationNumber = "TMS-2026-0001", Name = "Alice Smith", GPA = 3.8m, IsActive = true },
            new() { RegistrationNumber = "TMS-2026-0002", Name = "Bob Jones", GPA = 2.9m, IsActive = true },
            new() { RegistrationNumber = "TMS-2026-0003", Name = "Charlie Brown", GPA = 3.4m, IsActive = false },
            new() { RegistrationNumber = "TMS-2026-0004", Name = "Diana Prince", GPA = 3.9m, IsActive = true },
            new() { RegistrationNumber = "TMS-2026-0005", Name = "Evan Wright", GPA = 2.5m, IsActive = true }
        };

        context.Students.AddRange(students);

        var courses = new List<Course>
        {
            new() { Code = "CS-101", Title = "Introduction to Computer Science", Capacity = 30 },
            new() { Code = "CS-201", Title = "Data Structures and Algorithms", Capacity = 25 },
            new() { Code = "MAT-101", Title = "Calculus I", Capacity = 40 }
        };

        context.Courses.AddRange(courses);

        context.SaveChanges();

        var enrollments = new List<Enrollment>
        {
            new() { StudentId = students[0].Id, CourseId = courses[0].Id, Grade = 4.0m },
            new() { StudentId = students[0].Id, CourseId = courses[1].Id, Grade = 3.6m },
            new() { StudentId = students[1].Id, CourseId = courses[0].Id, Grade = 2.8m },
            new() { StudentId = students[3].Id, CourseId = courses[1].Id, Grade = 3.9m }
        };

        context.Enrollments.AddRange(enrollments);

        context.SaveChanges();
    }
    var demo = scope.ServiceProvider.GetRequiredService<NPlusOneDemo>();

      await demo.RunAsync();

      // Soft-delete the first student
     var softDeleteDemo =scope.ServiceProvider.GetRequiredService<SoftDeleteDemo>();
     await softDeleteDemo.RunAsync();
 
    var bulkArchive =scope.ServiceProvider.GetRequiredService<BulkArchiveDemo>();

     await bulkArchive.RunAsync();
<<<<<<< HEAD
}  */ 
// Start application
app.UseStatusCodePages();
app.MapControllers();
=======
}   */
// Start application
>>>>>>> 07650c5 (m6)
app.Run();
