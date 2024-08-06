namespace SchoolManagement.Domain.Entities;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ClassroomId { get; set; }
    public Classroom Classroom { get; set; }
    public ICollection<StudentGroup> StudentGroups { get; set; }
    public ICollection<GroupClassroom> GroupClassrooms { get; set; }
}