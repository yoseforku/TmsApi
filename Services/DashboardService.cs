using Microsoft.EntityFrameworkCore;
using TmsApi.Data;
using TmsApi.Entities;
namespace TmsApi.Services;

public class DashboardService
{
    private readonly TmsDbContext _context;

    public DashboardService(TmsDbContext context)
    {
        _context = context;
    }


    // TODO 1: Pagination
    public async Task<List<Student>> GetStudentsPaged(
        int page,
        CancellationToken cancellationToken)
    {
        int pageSize = 20;

        return await _context.Students
            .OrderBy(s => s.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }



    // TODO 2: Top 5 courses by enrollment count
 public async Task<IEnumerable<object>> GetTopCourses(
    CancellationToken cancellationToken)
{
    return await _context.Enrollments
        .GroupBy(e => e.CourseId)
        .Select(g => new
        {
            CourseId = g.Key,
            CourseTitle = g.First().Course.Title,
            EnrollmentCount = g.Count()
        })
        .OrderByDescending(x => x.EnrollmentCount)
        .Take(5)
        .ToListAsync(cancellationToken);
}
}