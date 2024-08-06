namespace SchoolManagement.Domain.Entities;

public class Classroom
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<GroupClassroom> GroupClassrooms { get; set; }
    public ICollection<TeacherClassroom> TeacherClassrooms { get; set; }
}