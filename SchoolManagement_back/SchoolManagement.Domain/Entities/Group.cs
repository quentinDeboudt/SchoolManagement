using System.Text.Json.Serialization;

namespace SchoolManagement.Domain.Entities;

public class Group
{
    public int Id { get; set; }
    public string? Name { get; set; }

    [JsonIgnore]
    public int ClassroomId { get; set; }
    public Classroom? Classroom { get; set; }
    [JsonIgnore]
    public ICollection<Person> Students { get; set; } = new List<Person>();
    [JsonIgnore]
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}