using System.Text.Json.Serialization;

namespace SchoolManagement.Domain.Entities;

public class Subject
{
    public int Id { get; set; }
    public string? Name { get; set; }

    [JsonIgnore]
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}