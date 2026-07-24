using Microsoft.AspNetCore.Mvc;
using TmsApi.Application.Dtos;
using TmsApi.Application.Interfaces;

namespace TmsApi.Api.Controllers;

[ApiController]
[Route("api/courses/{courseId:int}/enrollments")]
public class EnrollmentsController(
    ICourseService courseService,
    IEnrollmentService enrollmentService) : ControllerBase
{

    // GET: /api/courses/{courseId}/enrollments
    [HttpGet(Name = "ListCourseEnrollments")]
    public async Task<IActionResult> GetEnrollments(
        int courseId,
        CancellationToken ct)
    {
       var enrollments = await enrollmentService
    .GetByCourseAsync(courseId, ct);
        return Ok(enrollments);
    }


    // GET: /api/courses/{courseId}/enrollments
[HttpGet(Name = "ListCourseEnrollments")]
public async Task<IActionResult> GetEnrollment(
    int courseId,
    CancellationToken ct)
{
    var course = await courseService.GetByIdAsync(courseId, ct);

    if (course is null)
    {
        return NotFound();
    }

    var enrollments = await enrollmentService
        .GetByCourseAsync(courseId, ct);

    return Ok(enrollments);
}


    // POST: /api/courses/{courseId}/enrollments
    [HttpPost]
    public async Task<IActionResult> EnrollStudent(
        int courseId,
        EnrollStudentRequest request,
        CancellationToken ct)
    {
        var course = await courseService.GetByIdAsync(courseId, ct);

        if (course is null)
        {
            return NotFound();
        }

        if (course.EnrollmentCount >= course.MaxCapacity)
        {
            return Conflict(new ProblemDetails
            {
                Title = "Course is full",
                Detail = $"Course '{course.Title}' has reached its maximum capacity of {course.MaxCapacity}.",
                Status = StatusCodes.Status409Conflict
            });
        }

        var enrollment = await enrollmentService
            .CreateAsync(courseId, request, ct);

        return CreatedAtAction(
            nameof(GetEnrollment),
            new { courseId, id = enrollment.Id },
            enrollment);
    }
    
}