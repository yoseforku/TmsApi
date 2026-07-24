/* using Microsoft.Extensions.DependencyInjection;
using TmsApi.Services;

public class EnrollmentWorker(IServiceScopeFactory scopeFactory)
{
    public void ProcessBatch()
    {
        // Create a short-lived scope
        using var scope = scopeFactory.CreateScope();

        // Resolve the scoped service
        var enrollmentService =
            scope.ServiceProvider.GetRequiredService<IEnrollmentService>();

        // Use the service
        var enrollments = enrollmentService.GetAllAsync().Result;

        Console.WriteLine($"Found {enrollments.Count()} enrollments.");

        // Scope is automatically disposed here
    }
} */

using TmsApi.Application.Interfaces;

namespace TmsApi.Api;

public class EnrollmentWorker(IServiceScopeFactory scopeFactory)

{

    public async Task ProcessBatch()

    {

        // Create a short-lived scope

        using var scope = scopeFactory.CreateScope();



        // Resolve scoped service

        var enrollmentService =

            scope.ServiceProvider.GetRequiredService<IEnrollmentService>();



        // Test course enrollments

        var courseId = 30;



        var enrollments = await enrollmentService

            .GetByCourseAsync(courseId, CancellationToken.None);



        Console.WriteLine(

            $"Found {enrollments.Count} enrollments for course {courseId}.");

    }

}