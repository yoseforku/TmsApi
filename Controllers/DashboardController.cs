using Microsoft.AspNetCore.Mvc;
using TmsApi.Services;

namespace TmsApi.Controllers;

[ApiController]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly DashboardService _service;

    public DashboardController(DashboardService service)
    {
        _service = service;
    }


    // Exercise 3 - Pagination
    [HttpGet("students")]
    public async Task<IActionResult> GetStudents(
        int page = 1,
        CancellationToken token = default)
    {
        var students = await _service.GetStudentsPaged(page, token);

        return Ok(students);
    }


    // Exercise 3 - GroupBy + Count
    [HttpGet("top-courses")]
    public async Task<IActionResult> GetTopCourses(
        CancellationToken token)
    {
        var courses = await _service.GetTopCourses(token);

        return Ok(courses);
    }
}