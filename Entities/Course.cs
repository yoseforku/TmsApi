namespace TmsApi.Entities;
public class Course
{
public int Id { get; set; }

public required string Code { get; set; } 
public required string Title { get; set; }
public int MaxCapacity { get; set; }
<<<<<<< HEAD

=======
// Navigation property for many-to-many relationship
>>>>>>> 07650c5 (m6)
public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
