#error TESTING_PROGRAM_CS

using Microsoft.AspNetCore.Authentication;
using Scalar.AspNetCore;
using TmsApi.Options;

var builder = WebApplication.CreateBuilder(args);


// =======================
// Services
// =======================

builder.Services.AddControllers();


// OpenAPI + Scalar support
builder.Services.AddOpenApi();


// Standard error responses
builder.Services.AddProblemDetails();


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


// Start application
app.Run();