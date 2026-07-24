namespace TmsApi.Infrastructure.Services;

public record EnrollmentRecord(
    string Id,
    string StudentId,
    string CourseCode,
    DateTime EnrolledAt);
    