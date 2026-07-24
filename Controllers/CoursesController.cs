<<<<<<< HEAD
/* using Microsoft.AspNetCore.Mvc;
=======
using Microsoft.AspNetCore.Mvc;
>>>>>>> 07650c5 (m6)
using TmsApi.Entities;
using TmsApi.Services;

namespace TmsApi.Controllers;

[ApiController]
[Route("api/courses")]
public class CoursesController(ICourseService courseService) : ControllerBase
{
    [HttpGet("{id:int}", Name = nameof(GetCourseById))]
    public async Task<IActionResult> GetCourseById(int id, CancellationToken ct)
    {
        var course = await courseService.GetByIdAsync(id, ct);

<<<<<<< HEAD
        if (course is null)
        {
            return NotFound();
        }

        return Ok(course);
=======
        if (course is not null)
        {
            return Ok(course);
        }

        return NotFound();
>>>>>>> 07650c5 (m6)
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse(Course course, CancellationToken ct)
    {
        var result = await courseService.CreateAsync(course, ct);

<<<<<<< HEAD
        return CreatedAtAction(
            nameof(GetCourseById),
            new { id = result.Id },
            result
        );
    }
} */

using Microsoft.AspNetCore.Mvc;
using TmsApi.Dtos;
using TmsApi.Services;

namespace TmsApi.Controllers;

[ApiController]
[Route("api/courses")]
public class CoursesController(ICourseService courseService) : ControllerBase
{
    [HttpGet("{id:int}", Name = nameof(GetCourseById))]
    public async Task<IActionResult> GetCourseById(int id, CancellationToken ct)
    {
        var course = await courseService.GetByIdAsync(id, ct);

        return course is not null ? Ok(course) : NotFound();
    }

    [HttpPost]
public async Task<IActionResult> CreateCourse(CreateCourseRequest request, CancellationToken ct)
{
    // Check if the course code already exists
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
=======
        return CreatedAtAction(nameof(GetCourseById), new { id = result.Id }, result);
    }
>>>>>>> 07650c5 (m6)
}