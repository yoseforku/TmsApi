using Microsoft.EntityFrameworkCore;
using TmsApi.Data;

namespace TmsApi.Services;

public class SoftDeleteDemo
{
    private readonly TmsDbContext _db;

    public SoftDeleteDemo(TmsDbContext db)
    {
        _db = db;
    }

    public async Task RunAsync()
    {
        Console.WriteLine("===== SOFT DELETE TEST =====");

        // 1. Soft delete a student
        var student = await _db.Students.FirstAsync();

        student.IsDeleted = true;

        await _db.SaveChangesAsync();

        Console.WriteLine($"Deleted: {student.Name}");


        // 2. Normal query (filter applied)
        var normalStudents = await _db.Students
            .ToListAsync();

        Console.WriteLine(
            $"Normal query count: {normalStudents.Count}"
        );


        // 3. Admin query (ignore filter)
        var allStudents = await _db.Students
            .IgnoreQueryFilters()
            .ToListAsync();

        Console.WriteLine(
            $"Admin query count: {allStudents.Count}"
        );
    }
}