using System.Text.Json.Serialization;


namespace SchoolManagement.Domain.Entities;
public class Role
{
    public int Id { get; set; }
    public string? Name { get; set; }
    
    [JsonIgnore]
    public ICollection<Person> Persons { get; set; } = new List<Person>();
}