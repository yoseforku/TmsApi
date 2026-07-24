using Microsoft.AspNetCore.Mvc;
using TmsApi.Application.Dtos;
using TmsApi.Application.Interfaces;

namespace TmsApi.Api.Controllers;

[ApiController]
[Route("api/courses")]
public class CoursesController(
    ICourseService courseService,
    LinkGenerator linkGenerator) : ControllerBase
{/* 
    [HttpGet("{id:int}", Name = nameof(GetCourseById))]
    public async Task<IActionResult> GetCourseById(
        int id,
        CancellationToken ct)
    {
        var course = await courseService.GetByIdAsync(id, ct);

        return course is not null
            ? Ok(course)
            : NotFound();
    } */
[HttpGet("{id:int}", Name = nameof(GetCourseById))]
public async Task<IActionResult> GetCourseById(
    int id,
    CancellationToken ct)
{
    var course = await courseService.GetByIdAsync(id, ct);

    if (course is null)
    {
        return NotFound();
    }


    // Build link to:
    // GET /api/courses/{courseId}/enrollments
    var enrollmentsUrl = linkGenerator.GetPathByName(
        HttpContext,
        "ListCourseEnrollments",
        new { courseId = id });


    if (enrollmentsUrl is null)
    {
        throw new InvalidOperationException(
            "ListCourseEnrollments route was not found.");
    }


    var links = new List<LinkDto>
    {
        new(
            linkGenerator.GetPathByName(
                HttpContext,
                nameof(GetCourseById),
                new { id })!,
            "self",
            "GET"),


        new(
            linkGenerator.GetPathByName(
                HttpContext,
                nameof(GetCourseById),
                new { id })!,
            "update",
            "PUT"),


        new(
            linkGenerator.GetPathByName(
                HttpContext,
                nameof(GetCourseById),
                new { id })!,
            "delete",
            "DELETE"),


        new(
            enrollmentsUrl,
            "enrollments",
            "GET")
    };


    // Add enroll link only when there is available capacity
    if (course.EnrollmentCount < course.MaxCapacity)
    {
        links.Add(
            new(
                enrollmentsUrl,
                "enroll",
                "POST"));
    }


    var detailDto = new CourseDetailDto
    {
        Id = course.Id,
        Code = course.Code,
        Title = course.Title,
        MaxCapacity = course.MaxCapacity,
        EnrollmentCount = course.EnrollmentCount,
        Links = links
    };


    return Ok(detailDto);
}
    [HttpPost]
    public async Task<IActionResult> CreateCourse(
        CreateCourseRequest request,
        CancellationToken ct)
    {
        if (await courseService.CodeExistsAsync(request.Code, ct))
        {
            return Conflict(new ProblemDetails
            {
                Title = "Course code already exists",
                Detail = $"A course with code '{request.Code}' is already registered.",
                Status = StatusCodes.Status409Conflict
            });
        }

        var result = await courseService.CreateAsync(request, ct);

        return CreatedAtAction(
            nameof(GetCourseById),
            new { id = result.Id },
            result);
    }


    [HttpGet]
    public async Task<IActionResult> GetCourses(
        [FromQuery] PagedRequest request,
        CancellationToken ct)
    {
        var result = await courseService.GetCoursesAsync(request, ct);

        return Ok(result);
    }
}