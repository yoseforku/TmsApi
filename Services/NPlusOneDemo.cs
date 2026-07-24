using Microsoft.EntityFrameworkCore;
using TmsApi.Data;

namespace TmsApi.Services;

public class NPlusOneDemo
{
    private readonly TmsDbContext _db;

    public NPlusOneDemo(TmsDbContext db)
    {
        _db = db;
    }

    public async Task RunAsync()
    {
        Console.WriteLine("===== PART A : N+1 Query =====");

        var students = await _db.Students
            .AsNoTracking()
            .ToListAsync();

        foreach (var s in students)
        {
            var count = await _db.Enrollments
                .AsNoTracking()
                .CountAsync(e => e.StudentId == s.Id);

            Console.WriteLine($"{s.Name}: {count} enrollments");
        }

        Console.WriteLine();
        Console.WriteLine("===== PART B : Optimized Query =====");

        var report = await _db.Students
            .AsNoTracking()
            .Select(s => new
            {
                s.Name,
                EnrollmentCount = s.Enrollments.Count
            })
            .ToListAsync();

        foreach (var r in report)
        {
            Console.WriteLine($"{r.Name}: {r.EnrollmentCount} enrollments");
        }
    }
}