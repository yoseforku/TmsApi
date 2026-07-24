using Microsoft.EntityFrameworkCore;
using TmsApi.Data;
using TmsApi.Entities;
<<<<<<< HEAD
using TmsApi.Dtos;
namespace TmsApi.Services;

public class CourseService(TmsDbContext context, ILogger<CourseService> logger): ICourseService
{
    /* public async Task<Course?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await context.Courses
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task<Course> CreateAsync(Course course, CancellationToken ct)
    {
        context.Courses.Add(course);

        await context.SaveChangesAsync(ct);

        logger.LogInformation("Course created with ID {Id}", course.Id);

        return course;
    } */
    public Task<CourseResponseDto?> GetByIdAsync(int id, CancellationToken ct) =>
    context.Courses
        .AsNoTracking()
        .Where(c => c.Id == id)
        .Select(c => new CourseResponseDto(
            c.Id,
            c.Code,
            c.Title,
            c.MaxCapacity,
            c.Enrollments.Count
        ))
        .FirstOrDefaultAsync(ct);

        public async Task<CourseResponseDto> CreateAsync(CreateCourseRequest request, CancellationToken ct)
{
    var course = new Course
    {
        Code = request.Code,
        Title = request.Title,
        MaxCapacity = request.MaxCapacity
    };

    context.Courses.Add(course);

    await context.SaveChangesAsync(ct);

    logger.LogInformation(
        "Created course {CourseId} ({Code})",
        course.Id,
        course.Code);

    return (await GetByIdAsync(course.Id, ct))!;
}
public Task<bool> CodeExistsAsync(string code, CancellationToken ct) =>
context.Courses.AsNoTracking().AnyAsync(c => c.Code == code, ct);
=======

namespace TmsApi.Services;
public class CourseService(TmsDbContext context, ILogger<CourseService>logger) : ICourseService
{
public async Task<Course?> GetByIdAsync(int id, CancellationToken ct)
{
// TODO 1: Use context.Courses.AsNoTracking()
// and return FirstOrDefaultAsync(c => c.Id == id, ct).

return await context.Courses
    .AsNoTracking()
    .FirstOrDefaultAsync(c => c.Id == id, ct);
throw new NotImplementedException();
} 
public async Task<Course> CreateAsync(Course course, CancellationToken ct)
{
  context.Courses.Add(course);

    await context.SaveChangesAsync(ct);

    return course;
    
    throw new NotImplementedException();
}
>>>>>>> 07650c5 (m6)
}