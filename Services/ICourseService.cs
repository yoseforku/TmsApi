<<<<<<< HEAD
using TmsApi.Dtos;
namespace TmsApi.Services;
public interface ICourseService
{
Task<CourseResponseDto?> GetByIdAsync(int id, CancellationToken ct);
Task<CourseResponseDto> CreateAsync(CreateCourseRequest request, CancellationToken ct);
Task<bool> CodeExistsAsync(string code, CancellationToken ct);
}
=======
using TmsApi.Entities;

namespace TmsApi.Services;

public interface ICourseService
{
Task<Course?> GetByIdAsync(int id, CancellationToken ct);

Task<Course> CreateAsync(Course course, CancellationToken ct);

} 
>>>>>>> 07650c5 (m6)
