using Microsoft.EntityFrameworkCore;
using TmsApi.Data;

namespace TmsApi.Services;

public class BulkArchiveDemo
{
    private readonly TmsDbContext _db;

    public BulkArchiveDemo(TmsDbContext db)
    {
        _db = db;
    }


    public async Task RunAsync()
    {
        Console.WriteLine("===== BULK ARCHIVE TEST =====");


        var cutoff = DateTime.UtcNow.AddDays(-30);


        var affectedRows = await _db.Enrollments
            .Where(e => e.EnrolledAt < cutoff)
            .ExecuteUpdateAsync(
                s => s.SetProperty(
                    e => e.IsArchived,
                    true
                ));


        Console.WriteLine(
            $"Archived rows: {affectedRows}"
        );
    }
}