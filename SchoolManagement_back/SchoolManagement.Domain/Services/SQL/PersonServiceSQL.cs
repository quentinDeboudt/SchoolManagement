// using SchoolManagement.Application.Interfaces;
// using SchoolManagement.Infrastructure;
// using SchoolManagement.Domain.Entities;
// using Microsoft.EntityFrameworkCore;


// namespace SchoolManagement.Domain.Services;

// public class PersonServiceSQL : IPersonService
// {
//     private readonly string _connectionString;

//     public PersonServiceSQL(string connectionString)
//     {
//         _connectionString = connectionString;
//     }

//     public IEnumerable<Person> GetAll()
//     {
//         var persons = new List<Person>();

//         using (var connection = new SqlConnection(_connectionString))
//         {
//             connection.Open();
//             var query = "SELECT * FROM Persons";

//             using (var command = new SqlCommand(query, connection))
//             {
//                 using (var reader = command.ExecuteReader())
//                 {
//                     while (reader.Read())
//                     {
//                         var person = new Person
//                         {
//                             Id = reader.GetInt32(0),
//                             FirstName = reader.GetString(1),
//                             LastName = reader.GetString(2)
//                         };
//                         persons.Add(person);
//                     }
//                 }
//             }
//         }

//         return persons;
//     }

//     public Person GetById(int id)
//     {
//         Person person = null;

//         using (var connection = new SqlConnection(_connectionString))
//         {
//             connection.Open();
//             var query = "SELECT * FROM Persons WHERE Id = @Id";

//             using (var command = new SqlCommand(query, connection))
//             {
//                 command.Parameters.AddWithValue("@Id", id);

//                 using (var reader = command.ExecuteReader())
//                 {
//                     if (reader.Read())
//                     {
//                         person = new Person
//                         {
//                             Id = reader.GetInt32(0),
//                             FirstName = reader.GetString(1),
//                             LastName = reader.GetString(2)
//                         };
//                     }
//                 }
//             }
//         }

//         return person;
//     }

//     public void Create(Person person)
//     {
//         using (var connection = new SqlConnection(_connectionString))
//         {
//             connection.Open();
//             var query = "INSERT INTO Persons (FirstName, LastName) VALUES (@FirstName, @LastName)";

//             using (var command = new SqlCommand(query, connection))
//             {
//                 command.Parameters.AddWithValue("@FirstName", person.FirstName);
//                 command.Parameters.AddWithValue("@LastName", person.LastName);
//                 command.ExecuteNonQuery();
//             }
//         }
//     }

//     public void Update(int id, Person person)
//     {
//         using (var connection = new SqlConnection(_connectionString))
//         {
//             connection.Open();
//             var query = "UPDATE Persons SET FirstName = @FirstName, LastName = @LastName WHERE Id = @Id";

//             using (var command = new SqlCommand(query, connection))
//             {
//                 command.Parameters.AddWithValue("@Id", id);
//                 command.Parameters.AddWithValue("@FirstName", person.FirstName);
//                 command.Parameters.AddWithValue("@LastName", person.LastName);
//                 command.ExecuteNonQuery();
//             }
//         }
//     }

//     public void Delete(int id)
//     {
//         using (var connection = new SqlConnection(_connectionString))
//         {
//             connection.Open();
//             var query = "DELETE FROM Persons WHERE Id = @Id";

//             using (var command = new SqlCommand(query, connection))
//             {
//                 command.Parameters.AddWithValue("@Id", id);
//                 command.ExecuteNonQuery();
//             }
//         }
//     }
// }