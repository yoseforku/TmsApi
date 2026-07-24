using Microsoft.Extensions.Logging;
<<<<<<< HEAD
using TmsApi.Dtos;
using TmsApi.Services;
=======

public interface IEnrollmentService
{
    Task<EnrollmentRecord> EnrollAsync(string studentId, string courseCode);
    Task<EnrollmentRecord?> GetByIdAsync(string id);
    Task<IReadOnlyList<EnrollmentRecord>> GetAllAsync();
    Task<bool> DeleteAsync(string id);
}
>>>>>>> 07650c5 (m6)


//--- The in-memory implementation ---
public class EnrollmentService : IEnrollmentService
{
    private readonly Dictionary<string, EnrollmentRecord> _store = new();
    private readonly ILogger<EnrollmentService> _logger;

    public EnrollmentService(ILogger<EnrollmentService> logger)
    {
        _logger = logger;
    }


    public Task<EnrollmentRecord> EnrollAsync(string studentId, string courseCode)
    {
        // Check for duplicate enrollment
        var existing = _store.Values
            .FirstOrDefault(e =>
                e.StudentId == studentId &&
                e.CourseCode == courseCode);

        if (existing is not null)
        {
            _logger.LogWarning(
                "Duplicate enrollment attempt {StudentId} already in {CourseCode} (record {EnrollmentId})",
                studentId,
                courseCode,
                existing.Id);

            return Task.FromResult(existing);
        }


        var id = Guid.NewGuid()
            .ToString("N")[..8];


        var record = new EnrollmentRecord(
            id,
            studentId,
            courseCode,
            DateTime.UtcNow);


        _store[id] = record;


        _logger.LogInformation(
            "Enrolled {StudentId} in {CourseCode} record {EnrollmentId}",
            studentId,
            courseCode,
            id);


        return Task.FromResult(record);
    }


    public Task<EnrollmentRecord?> GetByIdAsync(string id)
    {
        _store.TryGetValue(id, out var record);

        if (record is null)
        {
            _logger.LogWarning(
                "Enrollment {EnrollmentId} not found",
                id);
        }

        return Task.FromResult(record);
    }


    public Task<IReadOnlyList<EnrollmentRecord>> GetAllAsync()
    {
        IReadOnlyList<EnrollmentRecord> all = _store.Values.ToList();

        return Task.FromResult(all);
    }


    public Task<bool> DeleteAsync(string id)
    {
        var removed = _store.Remove(id);

        if (removed)
        {
            _logger.LogInformation(
                "Deleted enrollment {EnrollmentId}",
                id);
        }
        else
        {
            _logger.LogWarning(
                "Delete failed enrollment {EnrollmentId} not found",
                id);
        }

        return Task.FromResult(removed);
    }
<<<<<<< HEAD

    public Task<EnrollmentResponseDto?> GetByIdAsync(int courseId, int id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<EnrollmentResponseDto> CreateAsync(int courseId, EnrollStudentRequest request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

=======
>>>>>>> 07650c5 (m6)
}


//--- The data shape ---
public record EnrollmentRecord(
    string Id,
    string StudentId,
    string CourseCode,
    DateTime EnrolledAt);